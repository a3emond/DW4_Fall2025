CREATE DATABASE [main_db];
GO

USE [main_db];
GO

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    role NVARCHAR(20) NOT NULL DEFAULT 'user',
    created_at DATETIME DEFAULT GETDATE()
);
GO

-- Menu Items and Media
CREATE TABLE menu_items (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(200) NOT NULL,
    category NVARCHAR(50) NOT NULL CHECK (category IN (
        'Appetizer','Main','Side','Dessert','Drink','Liquor'
    )),
    description NVARCHAR(MAX) NULL,
    price DECIMAL(10,2) NOT NULL,
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE menu_item_media (
    id INT PRIMARY KEY IDENTITY(1,1),
    menu_item_id INT NOT NULL,
    file_name NVARCHAR(255) NOT NULL,
    media_type NVARCHAR(50) NOT NULL DEFAULT 'image',
    is_primary BIT NOT NULL DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (menu_item_id) REFERENCES menu_items(id) ON DELETE CASCADE
);
GO

-- Reservations and Guests
CREATE TABLE reservations (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    reservation_date DATETIME NOT NULL,  -- actual chosen date+time
    type NVARCHAR(20) NOT NULL CHECK (type IN ('DineIn','Preorder','Delivery','Estimate')),
    status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(id)
);
GO

CREATE TABLE reservation_guests (
    id INT PRIMARY KEY IDENTITY(1,1),
    reservation_id INT NOT NULL,
    guest_name NVARCHAR(100) NULL,
    FOREIGN KEY (reservation_id) REFERENCES reservations(id) ON DELETE CASCADE
);
GO

CREATE TABLE reservation_orders (
    id INT PRIMARY KEY IDENTITY(1,1),
    guest_id INT NOT NULL,
    menu_item_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    price DECIMAL(10,2) NOT NULL, -- snapshot of price at reservation time
    FOREIGN KEY (guest_id) REFERENCES reservation_guests(id) ON DELETE CASCADE,
    FOREIGN KEY (menu_item_id) REFERENCES menu_items(id)
);
GO

-- Reservation Time Blocks (Capacity Management)
CREATE TABLE reservation_timeblocks (
    id INT PRIMARY KEY IDENTITY(1,1),
    block_date DATE NOT NULL,
    block_start TIME NOT NULL,
    block_end TIME NOT NULL,
    capacity INT NOT NULL, -- max number of seats for this block
    created_at DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE reservation_block_allocations (
    id INT PRIMARY KEY IDENTITY(1,1),
    reservation_id INT NOT NULL,
    block_id INT NOT NULL,
    seats_reserved INT NOT NULL,
    FOREIGN KEY (reservation_id) REFERENCES reservations(id) ON DELETE CASCADE,
    FOREIGN KEY (block_id) REFERENCES reservation_timeblocks(id) ON DELETE CASCADE
);
GO

-- Menu items inserts (extrtacted via powershell reflexion command then sanitized by AI) i call it using tools!!

-- DESSERTS
INSERT INTO menu_items (name, category, description, price)
VALUES ('Millionaire Bars', 'Dessert', 'Layers of buttery shortbread, caramel, and chocolate.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '11747732-Millionaire-bars-recipe-4x3-d0bd2674af3b4c67ada42e6032b1bad3.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Strawberry Churro Roll-Ups', 'Dessert', 'Crispy churro-style rolls filled with strawberries and cream.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '11768320-Strawberry-Churro-Roll-Ups-ddmfs-beauty-4x3-4edb527d93d5435f8f279199eced54d9.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Apple Cannoli', 'Dessert', 'Crunchy cannoli shells filled with apple-spiced ricotta.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '11796432-Apple-Cannoli-DDMFS-Beauty-4x3-38a5309c768942bab09e081d3d3c1f58.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Pumpkin Chai Latte Bars', 'Dessert', 'Pumpkin bars with chai spices and creamy glaze.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '11801788_Pumpkin-Chai-Latte-Bars_Kim-Shupe_4x3-7aaf8d4cdc3d4ade92a1758d64029134.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Mini Chocolate Cakes', 'Dessert', 'Rich mini chocolate cakes with ganache topping.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Mini-chocolate-cakes_2.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Panna Cotta', 'Dessert', 'Silky Italian cream dessert served with berry coulis.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Panna-cotta_8-close-up.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Peanut Butter Brownies', 'Dessert', 'Fudgy brownies with a peanut butter swirl center.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Peanut-butter-stuffed-brownies_9.webp', 1);
GO


-- MAIN DISHES
INSERT INTO menu_items (name, category, description, price)
VALUES ('Asian Chilli Chicken', 'Main', 'Spicy stir-fried chicken with tangy chili sauce.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Asian-Chilli-Chicken_7a.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Baked Ziti', 'Main', 'Classic oven-baked pasta with tomato sauce and cheese.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Baked-Ziti.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Chinese Noodle Soup', 'Main', 'Comforting noodle soup with vegetables and broth.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Chinese-Noodle-Soup_0.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Filipino Chicken Adobo', 'Main', 'Chicken braised in soy sauce, vinegar, and spices.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Filipino-Chicken-Adobo_7.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Moroccan Stuffed Eggplant', 'Main', 'Roasted eggplant halves filled with spiced lamb or beef.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Moroccan-baked-eggplant-halves-with-beef-or-lamb_0.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Moroccan Lamb Meatballs', 'Main', 'Tender lamb meatballs simmered in aromatic spices.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Moroccan-lamb-meatballs_2.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('One-Pot Greek Risoni', 'Main', 'Flavorful risoni pasta cooked with herbs and vegetables.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'One-Pot-Greek-Risoni_7.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Pad See Ew', 'Main', 'Thai stir-fried noodles with soy sauce and vegetables.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Pad-See-Ew_2.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Keema Curried Beef', 'Main', 'Indian-style minced beef cooked with curry spices.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Queema-Indian-Curried-Beef-Mince_3.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Sausage Ragu', 'Main', 'Slow-cooked sausage ragu with pasta in rich tomato sauce.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Sausage-ragu_7.webp', 1);
GO

-- APPETIZERS
INSERT INTO menu_items (name, category, description, price)
VALUES ('Fried Pickles', 'Appetizer', 'Crispy golden pickles served with a tangy dipping sauce.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'air-fryer-fried-pickles-2.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Buffalo Cauliflower', 'Appetizer', 'Spicy battered cauliflower bites tossed in buffalo sauce.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'buffalo-cauliflower.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Caprese Skewers', 'Appetizer', 'Fresh mozzarella, cherry tomatoes, and basil on skewers.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'caprese-skewers-1.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Garlic Knots', 'Appetizer', 'Soft baked knots brushed with garlic butter and herbs.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'garlic-knots-1.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Garlic Prawns', 'Appetizer', 'Juicy prawns sautéed with garlic and herbs.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Garlic-Prawns_Shrimps.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Jalapeño Poppers', 'Appetizer', 'Crispy jalapeños stuffed with cheese and spices.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'jalapeno-poppers-1.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Stuffed Mushrooms', 'Appetizer', 'Baked mushrooms filled with herbed cheese stuffing.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'stuffed-mushrooms-1.jpg', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Thai Lettuce Wraps', 'Appetizer', 'Fresh lettuce wraps filled with seasoned chicken larb gai.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'Thai-Lettuce-Wraps_Larb-Gai_0.webp', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Zucchini Chips', 'Appetizer', 'Crispy baked zucchini slices with light seasoning.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'zucchini-chips.jpg', 1);
GO


-- SIDE DISHES
INSERT INTO menu_items (name, category, description, price)
VALUES ('Creamed Spinach', 'Side', 'Rich and creamy spinach cooked with garlic and cream.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '20190725-delish-creamed-spinach-ehg-horizontal-1-1565302326.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Maple Glazed Carrots', 'Side', 'Tender carrots glazed with maple syrup and miso.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, '210210-delish-vegan-cookbook-miso-maple-glazed-carrots-horizontal-0004-eb-1625781369.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Cranberry Bacon Green Beans', 'Side', 'Crisp green beans tossed with bacon and cranberries.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'cranberry-bacon-green-beans-lead-654e5406755d5.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Sweet Potato Fries', 'Side', 'Golden sweet potato fries with a crispy finish.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'del099923-sweet-potato-fries-web-0659-eb-hi-res-lead-6529d18360771.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Caramelized Brussels Sprouts', 'Side', 'Oven-roasted brussels sprouts glazed with balsamic.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'del109923-caramelized-brussel-sprouts-web-032-rl-hi-res-lead-65491018958d0.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Melting Sweet Potatoes', 'Side', 'Thick slices of sweet potato roasted to perfection.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'del109924-melting-sweet-potato-web-258-jg-lead-672c06e44ced8.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Greek Salad', 'Side', 'Refreshing salad with cucumber, tomato, olives, and feta.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'greek-salad-lead-642f29241cceb.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Roasted Halloumi & Broccolini', 'Side', 'Grilled halloumi cheese served with roasted broccolini.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'one-pan-roasted-halloumi-and-broccolini-lead-65bd0cdcc8d9f.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Roasted Potatoes', 'Side', 'Crispy oven-roasted potatoes with herbs and garlic.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'roasted-potatoes-lead-66b12d40121ff.avif', 1);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Smashed Brussels Sprouts', 'Side', 'Crispy smashed brussels sprouts with olive oil and seasoning.', 0.00);
DECLARE @id INT = SCOPE_IDENTITY();
INSERT INTO menu_item_media (menu_item_id, file_name, is_primary)
VALUES (@id, 'smashed-brussels-sprouts-lead-6733770d088f9.jpg', 1);
GO

-- DRINKS
INSERT INTO menu_items (name, category, description, price)
VALUES ('Coca-Cola', 'Drink', 'Classic refreshing cola served chilled.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Sparkling Water', 'Drink', 'Carbonated mineral water with a crisp taste.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Iced Tea', 'Drink', 'Freshly brewed iced tea with lemon.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Fresh Orange Juice', 'Drink', '100% squeezed orange juice.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Espresso', 'Drink', 'Strong Italian-style espresso shot.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Cappuccino', 'Drink', 'Espresso topped with steamed milk foam.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Mojito', 'Drink', 'Refreshing cocktail with rum, mint, lime, and soda.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Virgin Mojito', 'Drink', 'Non-alcoholic mojito with mint and lime.', 0.00);
GO


-- LIQUORS (Wines by glass/carafe/bottle)
INSERT INTO menu_items (name, category, description, price)
VALUES ('House Red Wine (Glass)', 'Liquor', 'Our signature house red wine served by the glass.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('House Red Wine (Carafe)', 'Liquor', 'Our signature house red wine served in a carafe.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('House Red Wine (Bottle)', 'Liquor', 'Our signature house red wine served by the bottle.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('House White Wine (Glass)', 'Liquor', 'Our signature house white wine served by the glass.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('House White Wine (Carafe)', 'Liquor', 'Our signature house white wine served in a carafe.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('House White Wine (Bottle)', 'Liquor', 'Our signature house white wine served by the bottle.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Prosecco', 'Liquor', 'Italian sparkling white wine.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Champagne Brut', 'Liquor', 'Premium French champagne with crisp bubbles.', 0.00);
GO


-- LIQUORS (Spirits)
INSERT INTO menu_items (name, category, description, price)
VALUES ('Whiskey (Neat)', 'Liquor', 'Classic whiskey served neat.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Whiskey Sour', 'Liquor', 'Whiskey shaken with lemon juice and sugar.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Gin & Tonic', 'Liquor', 'Refreshing gin mixed with tonic water and lime.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Rum & Coke', 'Liquor', 'Dark rum mixed with Coca-Cola.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Vodka Martini', 'Liquor', 'Classic martini with vodka and dry vermouth.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Tequila Sunrise', 'Liquor', 'Tequila with orange juice and grenadine.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Margarita', 'Liquor', 'Tequila, triple sec, and lime juice on the rocks.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Old Fashioned', 'Liquor', 'Whiskey, sugar, and bitters with orange zest.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Negroni', 'Liquor', 'Gin, vermouth, and Campari stirred over ice.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Irish Coffee', 'Liquor', 'Hot coffee blended with Irish whiskey and cream.', 0.00);
GO

INSERT INTO menu_items (name, category, description, price)
VALUES ('Baileys on Ice', 'Liquor', 'Irish cream liqueur served over ice.', 0.00);
GO


-- Update prices 
-- Desserts: $6–$9
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Millionaire Bars' THEN 8.00
        WHEN 'Strawberry Churro Roll-Ups' THEN 7.50
        WHEN 'Apple Cannoli' THEN 7.00
        WHEN 'Pumpkin Chai Latte Bars' THEN 7.50
        WHEN 'Mini Chocolate Cakes' THEN 8.50
        WHEN 'Panna Cotta' THEN 7.50
        WHEN 'Peanut Butter Brownies' THEN 6.50
        ELSE 7.00
    END
WHERE category = 'Dessert';

-- Main Dishes: $14–$22
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Asian Chilli Chicken' THEN 16.00
        WHEN 'Baked Ziti' THEN 15.00
        WHEN 'Chinese Noodle Soup' THEN 14.00
        WHEN 'Filipino Chicken Adobo' THEN 17.00
        WHEN 'Moroccan Stuffed Eggplant' THEN 18.00
        WHEN 'Moroccan Lamb Meatballs' THEN 19.00
        WHEN 'One-Pot Greek Risoni' THEN 16.00
        WHEN 'Pad See Ew' THEN 15.00
        WHEN 'Keema Curried Beef' THEN 18.00
        WHEN 'Sausage Ragu' THEN 17.00
        ELSE 16.00
    END
WHERE category = 'Main';

-- Appetizers: $6–$10
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Fried Pickles' THEN 7.00
        WHEN 'Buffalo Cauliflower' THEN 8.00
        WHEN 'Caprese Skewers' THEN 9.00
        WHEN 'Garlic Knots' THEN 6.50
        WHEN 'Garlic Prawns' THEN 10.00
        WHEN 'Jalapeño Poppers' THEN 8.50
        WHEN 'Stuffed Mushrooms' THEN 9.00
        WHEN 'Thai Lettuce Wraps' THEN 9.50
        WHEN 'Zucchini Chips' THEN 6.50
        ELSE 8.00
    END
WHERE category = 'Appetizer';

-- Side Dishes: $4–$8
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Creamed Spinach' THEN 6.00
        WHEN 'Maple Glazed Carrots' THEN 6.50
        WHEN 'Cranberry Bacon Green Beans' THEN 7.00
        WHEN 'Sweet Potato Fries' THEN 5.50
        WHEN 'Caramelized Brussels Sprouts' THEN 7.00
        WHEN 'Melting Sweet Potatoes' THEN 6.50
        WHEN 'Greek Salad' THEN 7.50
        WHEN 'Roasted Halloumi & Broccolini' THEN 8.00
        WHEN 'Roasted Potatoes' THEN 5.00
        WHEN 'Smashed Brussels Sprouts' THEN 7.00
        ELSE 6.00
    END
WHERE category = 'Side';

-- Drinks (non-alcoholic): $2–$5
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Coca-Cola' THEN 3.00
        WHEN 'Sparkling Water' THEN 3.50
        WHEN 'Iced Tea' THEN 3.50
        WHEN 'Fresh Orange Juice' THEN 4.50
        WHEN 'Espresso' THEN 2.50
        WHEN 'Cappuccino' THEN 4.00
        WHEN 'Mojito' THEN 5.00
        WHEN 'Virgin Mojito' THEN 4.50
        ELSE 3.50
    END
WHERE category = 'Drink';

-- Liquors (Wines & Champagne): by format
UPDATE menu_items
SET price =
    CASE name
        WHEN 'House Red Wine (Glass)' THEN 7.00
        WHEN 'House Red Wine (Carafe)' THEN 18.00
        WHEN 'House Red Wine (Bottle)' THEN 28.00
        WHEN 'House White Wine (Glass)' THEN 7.00
        WHEN 'House White Wine (Carafe)' THEN 18.00
        WHEN 'House White Wine (Bottle)' THEN 28.00
        WHEN 'Prosecco' THEN 32.00
        WHEN 'Champagne Brut' THEN 65.00
        ELSE price
    END
WHERE category = 'Liquor';

-- Liquors (Spirits & Cocktails): fixed prices
UPDATE menu_items
SET price =
    CASE name
        WHEN 'Whiskey (Neat)' THEN 9.00
        WHEN 'Whiskey Sour' THEN 10.00
        WHEN 'Gin & Tonic' THEN 9.00
        WHEN 'Rum & Coke' THEN 9.00
        WHEN 'Vodka Martini' THEN 12.00
        WHEN 'Tequila Sunrise' THEN 10.00
        WHEN 'Margarita' THEN 11.00
        WHEN 'Old Fashioned' THEN 12.00
        WHEN 'Negroni' THEN 11.00
        WHEN 'Irish Coffee' THEN 9.00
        WHEN 'Baileys on Ice' THEN 8.00
        ELSE price
    END
WHERE category = 'Liquor';
