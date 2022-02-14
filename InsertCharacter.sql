delete from characters;

ALTER TABLE characters auto_increment = 1;

insert into hungergames.characters (name, environment) values ("Andra", "Woods");
insert into hungergames.characters (name, environment) values ("Maddi", "Woods");
insert into hungergames.characters (name, environment) values ("Marci", "Woods");
insert into hungergames.characters (name, environment) values ("Tyra", "Woods");
insert into hungergames.characters (name, environment) values ("Kamilah", "Woods");
insert into hungergames.characters (name, environment) values ("Shiraori", "Woods");
insert into hungergames.characters (name, environment) values ("Raine", "Woods");
insert into hungergames.characters (name, environment) values ("Coco", "Woods");
insert into hungergames.characters (name, environment) values ("Blueberry", "Woods");
insert into hungergames.characters (name, environment) values ("Moriah", "Woods");
insert into hungergames.characters (name, environment) values ("Amalica", "Woods");
insert into hungergames.characters (name, environment) values ("Tidi", "Woods");
insert into hungergames.characters (name, environment) values ("Dmitry", "Woods");

update characters set environment = "FrogStomach" where id=2;

select * from characters;