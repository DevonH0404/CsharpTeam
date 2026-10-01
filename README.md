# LibraryApp — .NET Starter Project
This is your starting point for the team project. It's a small console app that
models a library book checkout system, with a test project already wired up
and one example test in place.

## What's in here

```
LibraryApp.sln                  ← open this in Visual Studio
LibraryApp/
  Book.cs                       ← a single book title, tracks copies available
  Catalog.cs                    ← manages the collection of books (add, search, checkout, return)
  Program.cs                    ← console app entry point, demonstrates basic usage
LibraryApp.Tests/
  BookTests.cs                  ← one example unit test — use this as a pattern
```

## Running the app

1. Open `LibraryApp.sln` in Visual Studio 2026.
2. Press **F5** (or the green Run arrow) to run the console app.
3. You should see the catalog print out, a book get checked out, and an updated count.

## Running the tests

1. Open the **Test menu → Test Explorer** in Visual Studio.
2. Click **Run All Tests**.
3. You should see `BookTests.CheckOut_ReducesAvailableCopiesByOne` pass (green checkmark).

## What you'll do with this

Over the semester you'll come back to this project to:
- Add more unit tests (Week 7)
- Add integration tests across `Catalog` and `Book` working together (Week 9)
- Add a smoke test confirming the app runs end-to-end without crashing (Week 10)
- Add a performance test (e.g., checking out many books quickly) (Week 13)
- Wire up GitHub Actions to run your tests automatically on every push (Week 11)

You're free to extend the `Catalog` and `Book` classes with new features as your
team project requires — this starter is meant to be a foundation, not a final product.

## TEAM MEMBERS
Devon Hornsby
Jb Lumawig Masadao
Joshua Lee
Kaydence Norris
