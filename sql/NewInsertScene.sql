use hungergames;

delete from scene;

ALTER TABLE scene auto_increment = 1;

insert into scene 
 (id, num_requirements, num_outcomes, num_participants, occurrences, priority, parent_scene_id, scene_name, description) values           
 (1,  0,                1,            1,                0,           1,        -1,              "Idle",     "")
,(2,  0,                1,            1,                0,           0,        -1,              "Forage",   "")
,(3,  0,                1,            1,                0,           0,        -1,              "Idle2",    "")
,(4,  0,                1,            1,                0,           0,        -1,              "Idle3",    "")
,(5,  0,                1,            2,                0,           1,        -1,              "ArmWrestle","")
;

select * from scene;