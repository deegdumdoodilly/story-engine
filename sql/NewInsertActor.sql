use hungergames;

delete from actor;

ALTER TABLE actor auto_increment = 1;

insert into actor (name, environment, id) values ("Andra", "Woods", 1);
insert into actor (name, environment, id) values ("Marci", "Woods", 2);
insert into actor (name, environment, id) values ("Shiraori", "Woods", 3);
insert into actor (name, environment, id) values ("Maddi", "Woods", 4);
insert into actor (name, environment, id) values ("Kamilah", "Woods", 5);
insert into actor (name, environment, id) values ("Tyra", "Woods", 6);
insert into actor (name, environment, id) values ("Blueberry", "Woods", 7);
insert into actor (name, environment, id) values ("Raine", "Woods", 8);

select * from actor;