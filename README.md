# modular-monolith-start

Getting started with modular monoliths

https://github.com/cvc-comanescu-catalin/modular-monolith-start.git

For this repo, go into to the relevant repo DIR and:
git config user.name "Your Name Here"
git config user.email your@email.example

git remote set-url origin git@github.com:cvc-comanescu-catalin/modular-monolith-start.git
git remote -v

# SSH for remote authentication

git config core.sshCommand "ssh -o IdentitiesOnly=yes -i ~/.ssh/id_cvc_ed25519"

# SSH for commit signing

git config user.signingKey "C:/Users/catalin/.ssh/id_cvc_ed25519.pub"
git config gpg.format ssh
git config commit.gpgsign true

mkdir src
cd src
dotnet new sln -n RiverBooks
dotnet new webapi -n RiverBooks.Web
dotnet sln RiverBooks.sln add .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet new classlib -n RiverBooks.Books -o RiverBooks.Books
dotnet sln RiverBooks.sln add .\RiverBooks.Books\RiverBooks.Books.csproj

github.com/ardalis/EditorConfig

dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet ef migrations add Initial -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations

dotnet ef database update
dotnet ef database update -- --environment Testing

dotnet ef migrations add InitialUsers -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
dotnet ef migrations add CartItems -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
dotnet ef database update -c UsersDbContext

dotnet ef migrations add InitialOrderProcessing -c OrderProcessingDbContext -p ..\RiverBooks.OrderProcessing\RiverBooks.OrderProcessing.csproj -s .\RiverBooks.Web.csproj -o Infrastructure/Data/Migrations
dotnet ef database update -c OrderProcessingDbContext

dotnet ef migrations add UserAddresses -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
dotnet ef database update -c UsersDbContext

docker run --name my-redis -p 6379:6379 -d redis
docker run --name=papercut -p 25:25 -p 37408:37408 jijiechen/papercut:latest -d
docker run --name mongodb -d -p 27017:27017 mongo
