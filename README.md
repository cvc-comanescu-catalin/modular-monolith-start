# modular-monolith-start

Getting started with modular monoliths

https://github.com/cvc-comanescu-catalin/modular-monolith-start.git

For this repo, go into to the relevant repo DIR and:
git config user.name "Your Name Here"
git config user.email your@email.example

git remote set-url origin git@github.com:cvc-comanescu-catalin/modular-monolith-start.git
git remote -v

# SSH for remote authentication

git config core.sshCommand 'ssh -o IdentitiesOnly=yes -i ~/.ssh/id_cvc_ed25519'

# SSH for commit signing

git config --global user.signingKey '~/.ssh/git_ed25519.pub'
git config --global gpg.format ssh
git config --global commit.gpgsign true

mkdir src
cd src
dotnet new sln -n RiverBooks
dotnet new webapi -n RiverBooks.Web
dotnet sln RiverBooks.sln add .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet new classlib -n RiverBooks.Books -o RiverBooks.Books
dotnet sln RiverBooks.sln add .\RiverBooks.Books\RiverBooks.Books.csproj
