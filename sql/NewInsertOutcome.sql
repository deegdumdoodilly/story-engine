use hungergames;

delete from outcome;

ALTER TABLE outcome auto_increment = 1;

insert into outcome (id, scene_id, type, effect, description)
values              (1,  1,        0,    "",     "None")
			       ,(2,  2,        0,    "",     "None")
			       ,(3,  3,        0,    "",     "None")
			       ,(4,  4,        0,    "",     "None")
			       ,(5,  5,        0,    "",     "None")
;

select * from outcome;