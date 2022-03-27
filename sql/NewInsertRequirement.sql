use hungergames;

delete from requirement;

ALTER TABLE requirement auto_increment = 1;

insert into requirement (id, scene_id, requirement) values (1, 3, "character 1 status is tired");

select * from requirement;