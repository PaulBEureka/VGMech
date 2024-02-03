CREATE TABLE UserTable (
    user_id INT PRIMARY KEY,
    username NVARCHAR(50),
    password NVARCHAR(50)
);

CREATE TABLE CommentTable (
    comment_id INT PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES UserTable(user_id),
    comment NVARCHAR(500),
    comment_date DATE
);

CREATE TABLE GameRecordTable (
    record_id INT PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES UserTable(user_id),
    game_title NVARCHAR(50),
    ranking_date DATE
);
