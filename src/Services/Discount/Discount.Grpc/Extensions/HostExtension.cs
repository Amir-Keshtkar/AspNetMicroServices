using Npgsql;

namespace Discount.Grpc.Extensions {
    public static class HostExtension {
        public static IHost MigrateDatabase<T>(this IHost host, int? retry = 0) {
            int retryForAvailability = retry.Value;

            using(var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<T>>();
                try {
                    logger.LogInformation("Migrating Postgre Sql Database , Try:{1}", retry + 1);
                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand {
                        Connection = connection,
                    };
                    command.CommandText = "DROP TABLE IF EXISTS coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                                                              ProductName VARCHAR(24) NOT NULL,
                                                                                                              Description TEXT, 
                                                                                                              Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'Iphone Discount', 150) ";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 345) ";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated Postgre Sql Database");
                } catch(Exception ex) {
                    logger.LogError(ex, "An Error Occurred While Migrating Postgre Sql");

                    if(retryForAvailability < 50) {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<T>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }
    }
}
