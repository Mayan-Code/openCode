FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#Ustaw sie w kontenerze w folderze 
WORKDIR /app 
#Kopiuj calosc projektu 
COPY ./src ./
#Stworz deploy
RUN dotnet publish WebAPI -c Release -o out
#Ustaw sie w kontenerze w folderze 
WORKDIR /test
COPY . ./
#RUN dotnet publish WebAPI -c Release -o out
# run tests on docker build
#RUN dotnet test
#2. poziom budowania
# Tworzymy obraz na bazie paczki runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ASPNETCORE_TEST="Wartosc testowa compose 163"
#Ustaw sie w kontenerze w folderze. - FROM to nowy byt. 
WORKDIR /app 
#Skopiuj z poprzedniego obrazu build 
COPY --from=build app/out .
#uruchomienie aplikacji
ENTRYPOINT dotnet WebAPI.dll "parametr z konsoli"