delete from outcome;

ALTER TABLE outcome auto_increment = 1;

INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(1, 1, 0, "", "{1} manages to flee, with a sticky tongue just on {her} heels");
INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(2, 1, -1, "1.environment=Frog Stomach, 1.state+Swallowed", "{1} is just a little too slow, and manages to get snagged! With a hearty swallow, the frog drags its meal down its gullet.");

INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(3, 2, 1, "1.status-Swallowed, 1.environment=Woods", "With a few hours of determined wiggling, {1} manages to finally pull {herself} free while the huge amphibian snoozing!");
INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(4, 2, 0, "", "The frog remains totally unaware of {1}'s desperate struggles for {her} life, croaking lazily on the banks of the pond.");
INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(5, 2, -1, "1.environment=Frog's Stomach, 1.status-Swallowed, 1.status+Digesting", "Despite {her} noble attempts, exhaustion finally sets in, and {1} is worked down in the frog's guts until {she}'s nothing but slime.");

INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(6, 3, 0, "", "Some more of {1} is drunk up through the winding bowels.");

INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(7, 4, 0, "", "{1} stalks {2} for a few more hours before deciding to find something more interesting to do.");
INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(8, 4, 0, "2.status+Injured", "{1} finds the moment to pounce... but {2} manages to defend herself and flees! Not without taking a few hits first.");
INSERT IGNORE INTO outcome (id, scene_id, type, effect, description) values 
	(9, 4, -1, "2.status+Swallowed, 2.environment={1}'s Stomach", "{1} finds the moment to pounce... and pins down {2}! A few hungry swallows later and {2} is all gone, nothing but a squirming gut swinging from {1}'s hips.");

select * from outcome;