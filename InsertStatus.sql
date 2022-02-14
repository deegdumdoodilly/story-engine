delete from statuses;

ALTER TABLE statuses auto_increment = 1;

insert into statuses (character_id, status) values (2, "Digesting");

insert into statuses (character_id, status) values (1, "HORNY");

select * from statuses;

delete from statuses where character_id = 1 and status = 'HORNY';