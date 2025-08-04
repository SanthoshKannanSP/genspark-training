FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrate

WORKDIR /src

COPY ./backend/AttendanceApi/. .

RUN pwd && ls -l


RUN dotnet restore AttendanceApi.csproj


# Install dotnet-ef CLI tool
RUN dotnet tool install --global dotnet-ef

# Make sure dotnet tools path is available
ENV PATH="$PATH:/root/.dotnet/tools"

# Restore dependencies


# Run EF Core database update
CMD ["dotnet", "ef", "database", "update"]