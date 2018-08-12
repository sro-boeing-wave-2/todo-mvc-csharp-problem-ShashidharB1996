FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 64426
EXPOSE 44333

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY keep.csproj ./
RUN dotnet restore 
COPY . .
WORKDIR /src
RUN dotnet build keep.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish keep.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "keep.dll"]