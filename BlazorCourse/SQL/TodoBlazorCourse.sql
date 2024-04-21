DROP DATABASE IF EXISTS TodoBlazorCourse;
CREATE DATABASE TodoBlazorCourse;
USE TodoBlazorCourse;

DROP TABLE IF EXISTS TodoItem;


/*public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    // modeled with a self-referencing relationship (parent-child), this is implemented with the foreign key ParentId
    public List<TodoItem> Items { get; set; } = new List<TodoItem>();
}*/

CREATE TABLE TodoItem
(
    Id          INT PRIMARY KEY AUTO_INCREMENT,
    ParentId    INT          NULL REFERENCES TodoItem (Id),
    Title       VARCHAR(100) NOT NULL,
    Description TEXT,
    IsDone      BOOLEAN      NOT NULL,
    CreatedAt   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CompletedAt DATETIME
);

INSERT INTO TodoItem (Title, Description, IsDone, CreatedAt, CompletedAt)
VALUES ('First Todo', 'This is the first todo', false, '2021-01-01 00:00:00', NULL);
INSERT INTO TodoItem (Title, Description, IsDone, CreatedAt, CompletedAt)
VALUES ('Second Todo', 'This is the second todo', false, '2021-01-02 00:00:00', NULL);
SET @parentIdSecondTodo = (SELECT LAST_INSERT_ID());
INSERT INTO TodoItem (Title, ParentId, Description, IsDone, CreatedAt, CompletedAt)
VALUES ('Second Todo - Child 1', @parentIdSecondTodo, 'Second Todo - Child 1', false, '2021-01-03 00:00:00', NULL);
INSERT INTO TodoItem (Title, ParentId, Description, IsDone, CreatedAt, CompletedAt)
VALUES ('Second Todo - Child 2', @parentIdSecondTodo, 'Second Todo - Child 2', false, '2021-01-03 00:00:00', NULL);

INSERT INTO TodoItem (Title, Description, IsDone, CreatedAt, CompletedAt)
VALUES ('Third Todo', 'This is the third todo', false, '2021-01-03 00:00:00', NULL);

