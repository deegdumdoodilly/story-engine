delete from actor;

ALTER TABLE actor auto_increment = 1;

insert into hungergames.actor (name, environment, id) values ("Andra", "Woods", 1);
insert into hungergames.actor (name, environment, id) values ("Maddi", "Woods", 2);
insert into hungergames.actor (name, environment, id) values ("Marci", "Woods", 3);
insert into hungergames.actor (name, environment, id) values ("Tyra", "Woods", 4);
insert into hungergames.actor (name, environment, id) values ("Kamilah", "Woods", 5);
insert into hungergames.actor (name, environment, id) values ("Shiraori", "Woods", 6);
insert into hungergames.actor (name, environment, id) values ("Raine", "Woods", 7);
insert into hungergames.actor (name, environment, id) values ("Coco", "Woods", 8);
insert into hungergames.actor (name, environment, id) values ("Blueberry", "Woods", 9);
insert into hungergames.actor (name, environment, id) values ("Moriah", "Woods", 10);
insert into hungergames.actor (name, environment, id) values ("Amalica", "Woods", 11);
insert into hungergames.actor (name, environment, id) values ("Tidi", "Woods", 12);
insert into hungergames.actor (name, environment, id) values ("Dmitry", "Woods", 13);

update actor set environment = "FrogStomach" where id=2;

select * from actor;