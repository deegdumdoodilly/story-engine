delete from status;

ALTER TABLE status auto_increment = 1;

insert into status (id, character_id, status) values (1, 2, "Digesting");

insert into status (id, character_id, status) values (2, 1, "HORNY");

select * from status;