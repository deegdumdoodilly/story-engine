delete from requirements;

ALTER TABLE requirements auto_increment = 1;

insert into requirements (requirement_scene_id, requirement, role) values (1, "environment=Woods", 1);
insert into requirements (requirement_scene_id, requirement, role) values (2, "environment=FrogStomach", 1);
insert into requirements (requirement_scene_id, requirement, role) values (3, "environment=FrogStomach", 1);
insert into requirements (requirement_scene_id, requirement, role) values (3, "status=Digesting", 1);
SELECT * FROM requirements;