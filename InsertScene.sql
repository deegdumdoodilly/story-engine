delete from scene;

ALTER TABLE scene auto_increment = 1;

insert into hungergames.scene (id, scene_name, num_requirements, num_outcomes, num_participants, description) 
values (1, "Giant Frog", 0, 2, 1, "{1} is suddenly confronted by a giant frog! It ribbits ominously.");

insert into hungergames.scene (id, scene_name, num_requirements, num_outcomes, num_participants, description) 
values (2, "Frog Guts", 1, 3, 1, "{1} is stewing in the frog's stomach. It's tight, sticky, and starting to look dire!");

insert into hungergames.scene (id, scene_name, num_requirements, num_outcomes, num_participants, description) 
values (3, "Frog Stew", 2, 0, 1, "{1} bubbles and burbles deep in amphibian intestines.");

insert into hungergames.scene (id, scene_name, num_requirements, num_outcomes, num_participants, description) 
values (4, "Ambush", 0, 3, 2, "While stalking the woods for prey, {1} spots {2}, who hasn\'t seemed to have noticed {her} yet.");

UPDATE scenes SET occurrences=0;

select * from hungergames.scene;
