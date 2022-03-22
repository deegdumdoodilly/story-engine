import mysql.connector
import logging
import random
import sys
import requests

api_url = 'http://localhost:8080/api'

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


#json is a dict object
def post_request_json(endpoint, json):
    headers = {
        "Content-Type": "application/json"
    }
    return requests.post(api_url + endpoint, json=json, headers=headers)


def post_request_params(endpoint, parameters):
    headers = {
        "Content-Type": "application/json"
    }
    return requests.post(api_url + endpoint, params=parameters, headers=headers)

def post_request_text(endpoint, text):
    headers = {
        "Content-Type": "text/plain"
    }
    return requests.post(api_url + endpoint, data=str(text), headers=headers)

def post_request(endpoint):
    headers = {
        "Content-Type": "application/json"
    }
    return requests.post(api_url + endpoint, headers=headers)


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
    statuses = sql_query("SELECT (status) FROM hungergames.status WHERE character_id={};".format(character_id))
    for s in statuses:
        if s[0] == status:
            return True
    return False


def get_random_voter(voting_frequency_dict):
    log("Selecting a random voter for this event.")
    voter_keys = list(voting_frequency_dict.keys())
    index = voter_keys[0]
    min_votes = voting_frequency_dict[index]
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
        if character[2] == requirement[12:]:
            return True
        else:
            log(
                "Rejected {}, role {} due to mismatched environment (required {}, actual {})".format(event_name, role,
                                                                                                       requirement[12:],
                                                                                                       character[2]))
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
        "SELECT scene_id, requirement, role FROM hungergames.requirement WHERE "
        "scene_id={} AND role={};".format(event[0], role))
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
    #text = text.replace('\'', '\\\'')

    for i in range(0, len(participant_ids)):
        name = sql_query('SELECT name FROM hungergames.actor WHERE id = {};'.format(participant_ids[i]))[0][0]
        text = text.replace('{' + str(i+1) + '}', name)

    return text


def create_and_publish_scenes():

    # Import begins
    characters = sql_query("SELECT id, name, environment, last_ate FROM hungergames.actor")
    global character_dict
    character_dict = {}
    for character in characters:
        character_dict[character[0]] = character

    events = sql_query("SELECT id, scene_name, num_requirements, num_outcomes, occurrences, num_participants FROM hungergames.scene")
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
        #command = "UPDATE hungergames.scene SET occurrences={} WHERE id={};".format(event_frequency[event_id], event_id)
        #sql_modify(command)
        post_request_text('/scene/' + str(event_id) + '/set-occurrences', event_frequency[event_id])

    votes_cast = {}
    voter_table = sql_query("SELECT id, voting_chances FROM hungergames.user WHERE valid_voter = 1;")

    # Make a record of votes cast
    for voter in voter_table:
        voter_id = voter[0]
        if voter[1] is None:
            votes_cast[voter_id] = 0
        else:
            votes_cast[voter_id] = voter[1]

    # Add to event stack
    for event_stack_index in range(0, len(event_stack)):
        stack_item = event_stack[event_stack_index]
        # Add to database stack
        log('Adding item to stack: {}'.format(stack_item))

        log("Inserting scene_stack with values ({}, 1, \"{}\");".format(stack_item['event_id'], str(stack_item['participants'])[1:-1]))
        indent_log()

        new_performance = {"sceneId": stack_item['event_id'], "inProgress": True, "participants": str(stack_item['participants'])[1:-1], "winningVote": None, "flavor": "", "time": -1}
        response_text = post_request_json('/performances/add', new_performance).text
        unindent_log()

        # Trim to get just the performanceId
        id_index = response_text.index('"id":') + len('"id":')
        response_text = response_text[id_index:]
        comma_index = response_text.index(",")
        response_text = response_text[:comma_index]

        performance_id = int(response_text)

        voter_id = get_random_voter(votes_cast)
        votes_cast[voter_id] += 1

        log("Inserting vote with performanceId {}, voterId {}".format(performance_id, voter_id))
        indent_log()
        new_vote = {"performanceId": performance_id, "voterId": voter_id, "hasChosenOutcome": False, "inProgress": True, "chosenOutcomeId": -1}
        post_request_json('/votes/add', new_vote)
        unindent_log()

    for key in votes_cast:
        post_request_text('/user/set-voting-chances?id=' + str(key), votes_cast[key])

    db.commit()


def choose_and_resolve_ballots():
    performances = sql_query("SELECT id, scene_id, participants FROM hungergames.performance WHERE in_progress = true;")
    # sql_modify("UPDATE hungergames.vote SET in_progress = false;")
    post_request_text('/votes/set-in-progress', False)
    # sql_modify("UPDATE hungergames.performance SET in_progress = false;")
    post_request_text('/performances/set-in-progress', False)

    log("Iterating through scene stack.")
    indent_log()
    # For each scene in the stack
    for performance in performances:
        performance_id = performance[0]
        log("Collecting completed ballots for performance #{}".format(performance_id))
        indent_log()
        votes = sql_query("SELECT voter_id, chosen_outcome_id, id FROM hungergames.vote WHERE performance_id = {} AND has_chosen_outcome=true;".format(performance_id))
        # Update voter status of all participants
        for ballot in votes:
            log("Looking at vote #" + str(ballot[2]) + ", which is a vote for outcome #" + str(ballot[1]))

            chosen_outcome_type = sql_query("SELECT type FROM hungergames.outcome WHERE id = {};".format(ballot[1]))[0][0]

            if chosen_outcome_type > 0:
                log("Tallying a positive vote for user #".format(ballot[0]))
                vote_type = "positive-votes"
            elif chosen_outcome_type < 0:
                log("Tallying a negative vote for user #".format(ballot[0]))
                vote_type = "negative-votes"
            else:
                log("Tallying a neutral vote for user #".format(ballot[0]))
                vote_type = "neutral-votes"
            post_request_text('users/' + '/modify-' + vote_type + "?id=" + str(ballot[0]), 1)

        unindent_log()
        if len(votes) == 0:
            # No one voted! Pick at random
            log("No one voted! Generating a random vote now.")
            indent_log()
            # Create a new vote tuple and insert it
            possible_outcomes = sql_query("SELECT id FROM hungergames.outcome WHERE scene_id = {};".format(performance[1]))
            chosen_outcome_id = random.sample(possible_outcomes, 1)[0][0]
            log("Chosen outcome is #{}".format(chosen_outcome_id))
            new_vote = {"performanceId": performance_id, "voterId": -1, "chosenOutcomeId": chosen_outcome_id, "inProgress": False, "hasChosenOutcome": True}
            response = post_request_json('/votes/add', new_vote).json()

            db.commit()
            new_vote_id = response["id"]
            winning_vote = (-1, chosen_outcome_id, new_vote_id)
            log("Inserted artificial vote, which is automatically the winning vote")
            unindent_log()
        else:
            winning_vote = random.sample(votes, 1)[0]
            log("Winning vote is #{}, which is a vote for outcome #{}".format(winning_vote[2], winning_vote[1]))

        # Update scene_stack
        log("Updating scene stack #{} to reflect this new winner (#{}).".format(performance_id, winning_vote[2]))
        response = post_request_text('/performances/' + '/set-winning-vote?id=' + str(performance_id), winning_vote[2])
        log("Updated performance entry: " + response.text)

        log("Applying event effects")
        effect_string = sql_query("SELECT effect FROM hungergames.outcome WHERE id = {};".format(winning_vote[1]))[0][0]

        log("string = '" + effect_string + "'")
        participants = performance[2].split(',')

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
                        new_status_text = effect[effect.index('+')+1:]
                        log("Adding status: \"{}\"".format(new_status_text))
                        new_status = {"characterId": character_id, "status": new_status_text}
                        post_request_json('/statuses/add', new_status)
                    elif '-' in effect:
                        old_status = effect[effect.index('-')+1:]
                        log("Removing status: \"{}\".".format(old_status))
                        sql_modify("DELETE FROM hungergames.status WHERE character_id = {} AND status = '{}';".format(character_id, old_status))
                    else:
                        log("Warning, did not detect + or - in effect status change.")
                    pass
                elif effect[effect.index('.')+1:].startswith('environment'):
                    new_environment = effect[effect.index('=')+1:]
                    new_environment = format_text(new_environment, participants)
                    log("Updating environment: \"{}\"".format(new_environment))
                    post_request_text('/actors/set-environment?id=' + str(character_id), new_environment)
                else:
                    log("Warning, did not detect 'status' or 'environment' for effect change")
                unindent_log()
            unindent_log()
    reset_indent()

    post_request('/time/advance')


if __name__ == '__main__':


    logging.basicConfig(filename='.log', filemode='w', level=logging.DEBUG, format='%(asctime)s %(message)s')

    random.seed(123)

    db = None

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
    log("Success, connection made.")

    if len(sys.argv) == 1:
        log_fatal("Error, you must provide at least one argument. Use 'help' to get a list of options.")

    for i in range(1, len(sys.argv)):
        inp = sys.argv[i].lower()
        log('Interpreting {}'.format(inp))
        if inp == 'help':
            print("This command takes a series of verbs as input, which can be any of the following: 'clear', "
                  "'assign' and 'resolve'. You can also provide an integer number which will be used as the seed for "
                  "any random generation that occurs in subsequent calculations. This can be used to achieve consistent"
                  " outcomes. The program will carry out the verb's meaning or assign the RNG seed in the sequence you "
                  "provide them in.\n\n"
                  "clear: resets the simulation, preserving only user profile information.\n"
                  "assign: retrieves a list of all actors in the simulation and attempts to assign appropriate scenes "
                  "to them. Scenes are published to the database as in-progress, and voting ballots are sent to random "
                  "users.\n"
                  "resolve: Takes all in-progress scenes and resolves them by picking randomly from the votes it "
                  "received. If no votes were received, an outcome is chosen randomly.")
        elif inp == 'clear':
            post_request('/scenes/reset')
            post_request('/users/reset')
            post_request('/time/reset')
            post_request('/performances/remove')
            post_request('/votes/remove')
            post_request('/statuses/remove')
            post_request_text('/actors/set-environment', 'Woods')
        elif inp == 'assign':
            create_and_publish_scenes()
        elif inp == 'resolve':
            choose_and_resolve_ballots()
            db.commit()
        else:
            try:
                inp = int(inp)
                random.seed(inp)
            except ValueError:
                msg = "Error, input '{}' was not recognized as an appropriate verb or seed for the random number " \
                      "generator. It will be ignored.".format(inp)
                log(msg)
                print(msg)

