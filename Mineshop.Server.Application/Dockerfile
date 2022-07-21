FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /application
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY ["Mineshop.Server.Application/Mineshop.Server.Application.csproj", "Mineshop.Server.Application/"]
COPY ["Mineshop.Server.Service/Mineshop.Server.Service.csproj", "Mineshop.Server.Service/"]
COPY ["Mineshop.Server.Domain/Mineshop.Server.Domain.csproj", "Mineshop.Server.Domain/"]
COPY ["Mineshop.Server.Model/Mineshop.Server.Model.csproj", "Mineshop.Server.Model/"]
COPY ["Mineshop.Server.Infrastructure/Mineshop.Server.Infrastructure.csproj", "Mineshop.Server.Infrastructure/"]
COPY ["Mineshop.Server.Payment/Mineshop.Server.Payment.csproj", "Mineshop.Server.Payment/"]
RUN dotnet restore "Mineshop.Server.Application/Mineshop.Server.Application.csproj"
COPY . .
WORKDIR "/source/Mineshop.Server.Application"
RUN dotnet build "Mineshop.Server.Application.csproj" -c Release -o /application/build

FROM build AS publish
RUN dotnet publish "Mineshop.Server.Application.csproj" -c Release -o /application/publish

FROM base AS final

ENV DATABASE_CONNECTION ""

ENV STRIPE_API_KEY ""
ENV STRIPE_WEBHOOK_SIGNING_KEY ""

WORKDIR /application
COPY --from=publish /application/publish .
ENTRYPOINT ["dotnet", "Mineshop.Server.Application.dll"]
