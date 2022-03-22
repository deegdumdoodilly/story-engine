delete from requirement;

ALTER TABLE requirement auto_increment = 1;

insert into requirement (id, scene_id, requirement, role) values (1, 1, "environment=Woods", 1);
insert into requirement (id, scene_id, requirement, role) values (2, 2, "environment=FrogStomach", 1);
insert into requirement (id, scene_id, requirement, role) values (3, 3, "environment=FrogStomach", 1);
insert into requirement (id, scene_id, requirement, role) values (4, 3, "status=Digesting", 1);
SELECT * FROM requirement;