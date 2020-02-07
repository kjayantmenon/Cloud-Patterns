FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Sched-Agent-Supv/Sched-Agent-Supv.csproj", "Sched-Agent-Supv/"]
RUN dotnet restore "Sched-Agent-Supv/Sched-Agent-Supv.csproj"
COPY . .
WORKDIR "/src/Sched-Agent-Supv"
RUN dotnet build "Sched-Agent-Supv.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sched-Agent-Supv.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sched-Agent-Supv.dll"]