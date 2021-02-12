# Git Workflow

1. When you need to make a change, check out a "feature branch" from `main`
   (first make sure `main` is up to date with remote)
    - For example, if your task is to add comments to blog posts, you might
      checkout a branch `blog-comments` from `main`
2. Make your changes on that branch, commit as you go
3. When you're done, make sure your branch is up to date with `main` and there
   are no conflicts, then
4. Push to the feature branch on the remote, and open a Pull Request with GitHub
    - Add a descriptive name and optionally a description to the PR
5. Assign at least one reviewer to the PR
6. Reviewer(s) and owner discuss the PR via the comments and GitHub's review
   feature
7. Once all comments are resolved, the PR can be merged at the next meeting with
   everyone present
    - Only merging at the meetings helps to keep everyone on the same page
    - If it's urgent, or if everyone is already aware of the PR, the PR can be
      merged outside of a meeting as long as everyone knows what is happening
    - For the merge option, generally choose to create a merge commit
        - If it's a small change with multiple commits, squashing might be a
          better option
        - For rebasing, this would have to be something that is decided on as a
          team, and we would have to keep it consistent
        - Generally just use your best judgement to decide on an option for your
          PR
8. Once the PR is merged into `main`, everyone should update their local repos,
   and merge/rebase in the changes to their feature branches asap