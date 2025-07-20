# Sample Data & Seeding

For local development and testing, you can use the following sample data to seed your database.

## Example User Seed (JSON)
```json
[
  {
    "id": "1",
    "username": "testuser",
    "email": "test@example.com",
    "role": "User"
  },
  {
    "id": "2",
    "username": "admin",
    "email": "admin@example.com",
    "role": "Admin"
  }
]
```

## How to Use
- Place this JSON in a file (e.g., `seed-users.json`).
- Update your `AppDbContext` or test setup to load and insert this data at startup.
- For integration tests, load this data before running tests.

## Example C# Seeding Code
```csharp
var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("seed-users.json"));
context.Users.AddRange(users);
context.SaveChanges();
```

---

Add more sample data for other entities as needed. 