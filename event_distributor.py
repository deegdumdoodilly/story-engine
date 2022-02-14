import mysql.connector
import logging
import random
import sys


def log_fatal(s):
    logging.error(s)
    print(s)
    exit(1)


indent = ''


def log(s):
    logging.info(indent + s)


def indent_log():
    global indent
    indent += '  '


def unindent_log():
    global indent
    if len(indent) != 0:
        indent = indent[2:]


def reset_indent():
    global indent
    indent = ''


def sql_query(query):
    try:
        cursor.execute(query)
        return cursor.fetchall()
    except mysql.connector.errors.ProgrammingError as e:
        log_fatal("Error, {}. Terminating process. Query was: \"{}\"".format(e, query))


def sql_modify(phrase):
    try:
        cursor.execute(phrase)
        # db.commit()
    except mysql.connector.errors.ProgrammingError as e:
        log_fatal("Error, {}. Terminating process. Query was: \"{}\"".format(e, phrase))


def has_status(character_id, status):
    statuses = sql_query("SELECT (status) FROM hungergames.statuses WHERE character_id={};".format(character_id))
    for s in statuses:
        if s[0] == status:
            return True
    return False


def get_random_voter(voting_frequency_dict):
    voter_keys = list(voting_frequency_dict.keys())
    min_votes = voting_frequency_dict[voter_keys[0]]
    for i in range(1, len(voter_keys)):
        min_votes = min(min_votes, voting_frequency_dict[voter_keys[i]])

    candidates = []
    for i in range(0, len(voter_keys)):
        if voting_frequency_dict[voter_keys[i]] == min_votes:
            candidates.append(voter_keys[i])

    return candidates[random.randrange(len(candidates))]


# Requirement is a string here
def meets_requirement(requirement, character, event_name, role):
    if requirement[:12] == "environment=":
        if character[3] == requirement[12:]:
            return True
        else:
            log(
                "Rejected {}, role {} due to mismatched environment (required {}, actual {})".format(event_name, role,
                                                                                                       requirement[12:],
                                                                                                       character[3]))
    elif requirement[:7] == "status=":
        if has_status(character[0], requirement[7:]):
            return True
        else:
            log(
                "Rejected {}, role {} due to missing required status ({})".format(event_name, role, requirement[7:]))
    else:
        log_fatal("Error, did not recognize input as either 'environment=' or 'status='.")


def meets_requirements(event, character, role):
    requirements = sql_query(
        "SELECT requirement_scene_id, requirement, role FROM hungergames.requirements WHERE "
        "requirement_scene_id={} AND role={};".format(event[0], role))
    includes_digesting = False
    for requirement in requirements:
        if not meets_requirement(requirement[1], character, event[1], role):
            return False
        elif requirement[1] == "status=Digesting":
            includes_digesting = True

    if has_status(character[0], "Digesting") and not includes_digesting:
        log(
            "Rejecting {} (role {}) since the character is digesting and role does not specifically allow it.".format(
                event[1], role))
        return False
    return True


# Stack will be defined as {event_id:int, participants:[]}
def add_character_to_stack(character, multiplayer_events=True):
    # Process: look at existing multiplayer events in the stack and try to add to one
    # If not, filter events and pick one at random. Create a stack entry
    global event_stack
    global event_frequency
    if multiplayer_events and len(event_stack) >= 1:
        log("Looking at existing multiplayer events")
        # For every item in the stack
        for i in range(0, len(event_stack)):
            stack_entry = event_stack[i]
            # For every role in that item
            for role in range(1, len(stack_entry['participants'])+1):
                # If the role is not taken
                if stack_entry['participants'][role-1] == -1:
                    # And we qualify
                    if meets_requirements(event_dict[stack_entry['event_id']], character, role):
                        # Then add our id
                        log('Joining event {} on stack, index {} in role {}'.format(
                            event_dict[stack_entry['event_id']][1], i, role))
                        event_stack[i]['participants'][role-1] = character[0]
                        event_frequency[stack_entry['event_id']] += 1
                        return

    # Now we attempt to create a new role

    candidate_event_roles = []  # event, role
    for event in event_dict.values():
        # Per event:
        if multiplayer_events or event[5] == 1:
            for role in range(1, event[5] + 1):
                if meets_requirements(event, character, role):
                    log("Adding event {}, role {}".format(event[1], role))
                    candidate_event_roles.append((event, role))
                else:
                    log("Rejecting event {} because role is not are available".format(event[1]))
        else:
            log("Rejecting event {} because e[5] == {}".format(event[1], event[5]))

    if len(candidate_event_roles) == 0:
        logging.warning("Warning, could not find a candidate event for " + character[1])
        return

    # Filter by frequency
    lowest_frequency = event_frequency[candidate_event_roles[0][0][0]]

    for i in range(1, len(candidate_event_roles)):
        lowest_frequency = min(event_frequency[candidate_event_roles[i][0][0]], lowest_frequency)

    i = 0
    while i < len(candidate_event_roles):
        if event_frequency[candidate_event_roles[i][0][0]] > lowest_frequency:
            log("Removing {} due to overuse".format(candidate_event_roles[i][0][1]))
            candidate_event_roles.pop(i)
        else:
            i += 1

    # Filter by complexity
    highest_complexity = candidate_event_roles[0][0][2]

    for i in range(1, len(candidate_event_roles)):
        if candidate_event_roles[i][0][2] > highest_complexity:
            highest_complexity = candidate_event_roles[i][0][2]

    i = 0
    while i < len(candidate_event_roles):
        if candidate_event_roles[i][0][2] < highest_complexity:
            log("Removing {} for inferior complexity".format(candidate_event_roles[i][0][1]))
            candidate_event_roles.pop(i)
        else:
            i += 1

    roll = random.randrange(0, len(candidate_event_roles))
    chosen_event_role = candidate_event_roles[roll]
    log("Candidates finalized, options are: " + str(
        [str(candidate_event_roles[x][0][1]) + ", role " + str(candidate_event_roles[x][1]) for x in
         range(0, len(candidate_event_roles))]))
    log("Randomly selecting event: {} (role {}). Pushing to stack at index {}".format(
        chosen_event_role[0][1], chosen_event_role[1], len(event_stack)))

    # Adding to stack
    participants_array = [-1] * chosen_event_role[0][5]
    participants_array[chosen_event_role[1]-1] = character[0]
    event_stack.append({'event_id': chosen_event_role[0][0], 'participants': participants_array})
    event_frequency[chosen_event_role[0][0]] += 1


def redistribute_stack_entry(stack_index):
    global event_stack
    global event_frequency
    event_id = event_stack[stack_index]['event_id']

    for character_id in event_stack[stack_index]['participants']:
        if character_id != -1:
            # Adjust frequency
            event_frequency[event_id] -= 1
            # Re-house character
            log("Selecting {}'s new event, single-person only".format(character_dict[character_id][1]))
            add_character_to_stack(character_dict[character_id], multiplayer_events=False)

    event_stack.pop(stack_index)


def format_text(text, participant_ids):
    text = text.replace('\'', '\\\'')

    for i in range(0, len(participant_ids)):
        name = sql_query('SELECT name FROM hungergames.characters WHERE id = {};'.format(participant_ids[i]))[0][0]
        text = text.replace('{' + str(i+1) + '}', name)

    return text


def chooseWinningBallots():
    stack_scenes = sql_query("SELECT id, scene_id, participants FROM hungergames.scene_stack WHERE in_progress = true;")
    sql_modify("UPDATE hungergames.votes SET in_progress = false;")
    sql_modify("UPDATE hungergames.scene_stack SET in_progress = false;")

    log("Iterating through scene stack.")
    indent_log()
    # For each scene in the stack
    for stack_scene in stack_scenes:
        stack_scene_id = stack_scene[0]
        log("Collecting completed ballots for stack_scene #{}".format(stack_scene_id))
        indent_log()
        votes = sql_query("SELECT voter_id, chosen_outcome, id FROM hungergames.votes WHERE scene_stack_id = {} AND has_chosen_outcome=true;".format(stack_scene_id))
        # Update voter status of all participants
        for ballot in votes:
            log("Looking at vote #" + ballot[2] + ", which is a vote for outcome #" + ballot[1])

            chosen_outcome_type = sql_query("SELECT type FROM hungergames.outcomes WHERE id = {};".format(ballot[1]))[0][0]

            if chosen_outcome_type > 0:
                log("Tallying a positive vote for user #".format(ballot[0]))
                vote_type = "positive_votes"
            elif chosen_outcome_type < 0:
                log("Tallying a negative vote for user #".format(ballot[0]))
                vote_type = "negative_votes"
            else:
                log("Tallying a neutral vote for user #".format(ballot[0]))
                vote_type = "neutral_votes"
            sql_modify("UPDATE hungergames.users SET {0} = {0} + 1 WHERE id = {1};".format(vote_type, ballot[0]))

        unindent_log()
        if len(votes) == 0:
            # No one voted! Pick at random
            log("No one voted! Generating a random vote now.")
            indent_log()
            # Create a new vote tuple and insert it
            possible_outcomes = sql_query("SELECT id FROM hungergames.outcomes WHERE outcome_scene_id = {};".format(stack_scene[1]))
            chosen_outcome_id = random.sample(possible_outcomes, 1)[0][0]
            log("Chosen outcome is #{}".format(chosen_outcome_id))
            sql_modify("INSERT INTO "
                       "hungergames.votes (scene_stack_id, voter_id, chosen_outcome, in_progress, has_chosen_outcome) "
                       "values ({}, Null, {}, false, true);".format(stack_scene_id, chosen_outcome_id))
            sql_modify("INSERT INTO hungergames.statuses (character_id, status) VALUES (2, 'Test status');")
            db.commit()
            new_vote_id = sql_query("SELECT id FROM hungergames.votes ORDER BY id DESC;")[0][0]
            winning_vote = (-1, chosen_outcome_id, new_vote_id)
            log("Inserted artificial vote, which is automatically the winning vote")
            unindent_log()
        else:
            winning_vote = random.sample(votes, 1)
            log("Winning vote is #{}, which is a vote for outcome #{}".format(winning_vote[2], winning_vote[1]))

        # Update scene_stack
        log("Updating scene stack #{} to reflect this new winner (#{}).".format(stack_scene_id, winning_vote[2]))
        sql_modify("UPDATE hungergames.scene_stack SET winning_vote = {} WHERE id = {}".format(winning_vote[2], stack_scene_id))

        log("Applying event effects")
        effect_string = sql_query("SELECT effect FROM hungergames.outcomes WHERE id = {};".format(winning_vote[1]))[0][0]

        log("string = '" + effect_string + "'")
        participants = stack_scene[2].split(',')

        if effect_string != '':
            indent_log()
            # Apply effects
            for effect in effect_string.split(','):
                log("Effect text: " + effect)
                effect = effect.strip()
                target_id = int(effect[:effect.index('.')]) - 1
                character_id = participants[target_id]
                log("Affecting character #{}".format(character_id))
                indent_log()
                if effect[effect.index('.')+1:].startswith('status'):
                    # Effect is a status change
                    if '+' in effect:
                        new_status = effect[effect.index('+')+1:]
                        log("Adding status: \"{}\"".format(new_status))
                        sql_modify("INSERT INTO hungergames.statuses (character_id, status) values ({}, '{}');".format(character_id, new_status))
                    elif '-' in effect:
                        old_status = effect[effect.index('-')+1:]
                        log("Removing status: \"{}\".".format(old_status))
                        sql_modify("DELETE FROM hungergames.statuses WHERE character_id = {} AND status = '{}';".format(character_id, old_status))
                    else:
                        log("Warning, did not detect + or - in effect status change.")
                    pass
                elif effect[effect.index('.')+1:].startswith('environment'):
                    new_environment = effect[effect.index('=')+1:]
                    new_environment = format_text(new_environment, participants)
                    log("Updating environment: \"{}\"".format(new_environment))
                    sql_modify("UPDATE hungergames.characters SET environment = '{}' WHERE id = {};".format(new_environment, character_id))
                else:
                    log("Warning, did not detect 'status' or 'environment' for effect change")
                unindent_log()
            unindent_log()
    reset_indent()



def create_and_publish_scenes():
    # Import begins
    characters = sql_query("SELECT * FROM hungergames.characters")
    global character_dict
    character_dict = {}
    for character in characters:
        character_dict[character[0]] = character

    events = sql_query("SELECT * FROM hungergames.scenes")
    global event_dict
    event_dict = {}
    for event in events:
        event_dict[event[0]] = event

    global event_frequency
    event_frequency = {}
    for e in events:
        event_frequency[e[0]] = e[4]

    global event_stack
    event_stack = []

    shuffled_characters = random.sample(characters, len(characters))

    # Pick events, store in dict with character id as key
    for character_tuple in shuffled_characters:
        log("Selecting {}'s event".format(character_tuple[1]))
        indent_log()
        add_character_to_stack(character_tuple)
        unindent_log()

    # Redistribute if needed
    for stack_index in range(0, len(event_stack)):
        event_id = event_stack[stack_index]['event_id']
        if -1 in event_stack[stack_index]['participants']:
            log('Missing slots found for event {}, stack position {}. Redistributing.'.format(
                         event_dict[event_id][1], stack_index))
            redistribute_stack_entry(stack_index)

    # Update frequencies on the database end
    for event_id in event_frequency.keys():
        command = "UPDATE hungergames.scenes SET occurrences={} WHERE id={};".format(event_frequency[event_id], event_id)
        sql_modify(command)

    votes_cast = {}
    voter_table = sql_query("SELECT id, voting_chances FROM hungergames.users WHERE valid_voter = 1;")

    # Make a record of votes cast
    for voter in voter_table:
        voter_id = voter[0]
        votes_cast[voter_id] = voter[1]

    # Add to event stack
    for event_stack_index in range(0, len(event_stack)):
        stack_item = event_stack[event_stack_index]
        # Add to database stack
        log('Adding item to stack: {}'.format(stack_item))

        log("Inserting scene_stack with values ({}, 1, \"{}\");".format(stack_item['event_id'], str(stack_item['participants'])[1:-1]))
        insert_phrase = "INSERT INTO hungergames.scene_stack (scene_id, in_progress, participants, winning_vote)" \
                        " VALUES ({}, 1, \"{}\", NULL);".format(stack_item['event_id'], str(stack_item['participants'])[1:-1])
        sql_modify(insert_phrase)
        stack_id = int(sql_query("SELECT id FROM hungergames.scene_stack ORDER BY id DESC;")[0][0])

        voter_id = get_random_voter(votes_cast)
        votes_cast[voter_id] += 1

        log("Inserting vote with values ({}, {}, 0);".format(stack_id, voter_id))
        insert_phrase = "INSERT INTO hungergames.votes (scene_stack_id, voter_id) VALUES ({}, {});" \
            .format(stack_id, voter_id)
        sql_modify(insert_phrase)
        stack_id += 1

    for key in votes_cast:
        modify_phrase = "UPDATE hungergames.users SET voting_chances={1} WHERE id={0};".format(key, votes_cast[key])
        sql_modify(modify_phrase)

    db.commit()
    exit(0)


if __name__ == '__main__':
    logging.basicConfig(filename='.log', filemode='w', level=logging.DEBUG, format='%(asctime)s %(message)s')

    random.seed(333)

    db = None

    log("Distributing events")
    try:
        db = mysql.connector.connect(host="hungergames-db.cwbqbtsmrk8y.us-east-1.rds.amazonaws.com",
                                     port="3306",
                                     user="admin",
                                     password="Password1!")
    except (mysql.connector.errors.InterfaceError, mysql.connector.errors.DatabaseError):
        log_fatal("Error, cannot connect to database, terminating process.")
    except mysql.connector.errors.ProgrammingError:
        log_fatal("Error, access denied for given username and password, terminating process.")

    global cursor
    cursor = db.cursor()
    log("Success, connection made. Attempting to read list of characters and events.")

    if len(sys.argv) > 1:
        if sys.argv[1].lower() == 'clear':
            sql_modify('UPDATE hungergames.scenes SET occurrences=0;')
            sql_modify('UPDATE hungergames.users SET '
                       'voting_chances=0, positive_votes=0, neutral_votes=0, negative_votes=0;')
            sql_modify('UPDATE hungergames.votes SET scene_stack_id = Null')
            sql_modify('DELETE FROM hungergames.scene_stack;')
            sql_modify('DELETE FROM hungergames.votes;')
        elif sys.argv[1].lower() == 'resolve':
            chooseWinningBallots()
            db.commit()
            exit(0)
        else:
            log_fatal('Error, did not understand command line input')
    create_and_publish_scenes()
    log('SUCCESS')

