
USE Reczept

-- Massa recept

INSERT INTO Recipe (Name) 
VALUES ('Pasta Carbonara')
INSERT INTO Recipe (Name) 
VALUES ('Korvstroganoff')
INSERT INTO Recipe (Name) 
VALUES ('Bruna bönor med fläsk')
INSERT INTO Recipe (Name) 
VALUES ('Lasange')
INSERT INTO Recipe (Name) 
VALUES ('Pannbiff med löksås')
INSERT INTO Recipe (Name) 
VALUES ('Kålpudding')
INSERT INTO Recipe (Name) 
VALUES ('Kalvdans')
INSERT INTO Recipe (Name) 
VALUES ('Pannkakor')
INSERT INTO Recipe (Name) 
VALUES ('Pho med kyckling')
INSERT INTO Recipe (Name) 
VALUES ('Kycklingspett med jordnötssås')
INSERT INTO Recipe (Name) 
VALUES ('Kycklingrullader men fetaost')
INSERT INTO Recipe (Name) 
VALUES ('Bouillabaisse')
INSERT INTO Recipe (Name) 
VALUES ('Panerad fisk med potatis')
INSERT INTO Recipe (Name) 
VALUES ('Rödbetsgravad lax')
INSERT INTO Recipe (Name) 
VALUES ('Fish and chips')
INSERT INTO Recipe (Name) 
VALUES ('Kyckling Provance')
INSERT INTO Recipe (Name) 
VALUES ('Coq au vin')
INSERT INTO Recipe (Name) 
VALUES ('Wokade grönsaker')
INSERT INTO Recipe (Name) 
VALUES ('Falafelwrap')
INSERT INTO Recipe (Name) 
VALUES ('Halloumipasta med majs och tomat')
INSERT INTO Recipe (Name) 
VALUES ('Tikkamasala med linser')

-- Taggar

INSERT INTO Tag (Name) 
VALUES ('Kött')
INSERT INTO Tag (Name) 
VALUES ('Fågel')
INSERT INTO Tag (Name) 
VALUES ('Fisk')
INSERT INTO Tag (Name) 
VALUES ('Vegetariskt')
INSERT INTO Tag (Name) 
VALUES ('Pasta')
INSERT INTO Tag (Name) 
VALUES ('Glutenfritt')
INSERT INTO Tag (Name) 
VALUES ('Franskt')
INSERT INTO Tag (Name) 
VALUES ('Husman')
INSERT INTO Tag (Name) 
VALUES ('Asiatiskt')
INSERT INTO Tag (Name) 
VALUES ('Förrätt')
INSERT INTO Tag (Name) 
VALUES ('Huvudrätt')
INSERT INTO Tag (Name) 
VALUES ('Efterrätt')
INSERT INTO Tag (Name) 
VALUES ('Italienskt')
INSERT INTO Tag (Name) 
VALUES ('Indiskt')


-- Taggar på recept

INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,1)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (13,1)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (5,1)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,1)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,2)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,2)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,2)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,2)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,3)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,3)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,3)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,3)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,4)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (13,4)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (5,4)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,4)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,5)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,5)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,5)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (1,6)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,6)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,6)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,6)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,7)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (12,7)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,7)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (4,8)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,8)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (12,8)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (2,9)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (9,9)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,9)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,9)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (2,10)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,10)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (9,10)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,10)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (2,11)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,11)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,11)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (3,12)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,12)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (7,12)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,12)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (3,13)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (8,13)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,13)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (3,14)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,14)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (10,14)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (3,15)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,15)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (2,16)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (7,16)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,16)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (2,17)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (7,17)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,17)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,17)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (4,18)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,18)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (9,18)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,18)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (4,19)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,19)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (4,20)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (5,20)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,20)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (4,21)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (6,21)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (11,21)
INSERT INTO TagsOnRecipe (TagId,RecipeId) 
VALUES (14,21)

-- Lägg till "användare" för testsyfte
insert into SlackUser (MemberId, Name)
values	('UJYN4N39S','Cajza'),
		('UJZMEGR5W','Marcus'),
		('UJYGD2D1B','Jens')

insert into Ingredient
values	('Pasta'),
		('Ägg'),
		('Vitlök'),
		('Olivolja'),
		('Smör'),
		('Bacon'),
		('Parmesan'),
		('Pecorino'),
		('Svartpeppar'),
		('Falukorv'),
		('Gul lök'),
		('Matlagningsgrädde'),
		('Vatten'),
		('Tomatpuré'),
		('Kinesisk soja'),
		('Salt'),
		('Bruna bönor (torkade)'),
		('Ljus sirap'),
		('Ättiksprit'),
		('Potatismjöl'),
		('Rimmat fläsk'),
		('Nötfärs'),
		('Timjan'),
		('Rosmarin'),
		('Krossade tomater'),
		('Köttbuljong'),
		('Lasagneplattor'),
		('Vetemjöl'),
		('Mjölk'),
		('Potatis'),
		('Ströbröd'),
		('Blandfärs'),
		('Dijonsenap'),
		('Strösocker'),
		('Persilja'),
		('Vitkål'),
		('Japansk soja'),
		('Vispgrädde'),
		('Kalvfond'),
		('Rårörda lingon'),
		('Råmjölk'),
		('Mandel'),
		('Kardemumma'),
		('Röd chili'),
		('Ingefära'),
		('Citrongräs'),
		('Nejlika'),
		('Kycklingfond'),
		('Tamarisoja'),
		('Fisksås'),
		('Glasnudlar'),
		('Purjolök'),
		('Pak choi'),
		('Shiitakesvamp'),
		('Kycklingfilé'),
		('Lime'),
		('Böngroddar'),
		('Mynta'),
		('Koriander'),
		('Cashewnötter'),
		('Palmsocker'),
		('Gurkmeja'),
		('Korianderpulver'),
		('Jordnötssmör'),
		('Kokosmjölk'),
		('Röd curry'),
		('Minutkycklingfilé'),
		('Basilika'),
		('Fetaost'),
		('Soltorkade tomater'),
		('Crème fraiche'),
		('Ris'),
		('Salladsmix'),
		('Körsbärstomater'),
		('Vinäger'),
		('Fänkål'),
		('Morötter'),
		('Vitt vin'),
		('Fiskbuljong'),
		('Saffran'),
		('Musslor'),
		('Blandad fisk i bit'),
		('Räkor'),
		('Surdegsbröd'),
		('Aioli'),
		('Broccoli'),
		('Rostad lök'),
		('Torskfilé'),
		('Chilisås'),
		('Tomater'),
		('Citronskal'),
		('Rödbetor'),
		('Laxfilé'),
		('Citronklyftor'),
		('Dill'),
		('Babyspenat'),
		('Rapsolja'),
		('Maizena'),
		('Bakpulver'),
		('Ljust öl'),
		('Citron'),
		('Blandade pickles'),
		('Frityrolja'),
		('Pommes frites'),
		('Gröna ärtor'),
		('Kyckling'),
		('Citronsaft'),
		('Provencalska örtkryddor'),
		('Rökt sidfläsk'),
		('Rödvin'),
		('Lagerblad'),
		('Schalottenlök'),
		('Champinjoner'),
		('Palsternacka'),
		('Salladskål'),
		('Sojabönor'),
		('Teriyakisås'),
		('Falafel'),
		('Tortillabröd'),
		('Rödlök'),
		('Vitlöksdressing'),
		('Halloumi'),
		('Majskorn'),
		('Pastasås'),
		('Röd paprika'),
		('Röda linser'),
		('Garam masala')