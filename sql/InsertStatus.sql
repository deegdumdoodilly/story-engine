delete from status;

ALTER TABLE status auto_increment = 1;

insert into status (id, actor_id, status) values (1, 1, "hungry");

select * from status;