using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRY",
                columns: table => new
                {
                    id_country = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    iso_code = table.Column<string>(type: "CHAR(2)", nullable: false),
                    continent = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRY", x => x.id_country);
                });

            migrationBuilder.CreateTable(
                name: "CITY",
                columns: table => new
                {
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_country = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    state = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    altitude_m = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    population = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    status = table.Column<string>(type: "CHAR(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITY", x => x.id_city);
                    table.ForeignKey(
                        name: "fk_city_country",
                        column: x => x.id_country,
                        principalTable: "COUNTRY",
                        principalColumn: "id_country",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INTEGRATION_LOG",
                columns: table => new
                {
                    id_log = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    api_name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    endpoint = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    http_method = table.Column<string>(type: "CHAR(6)", nullable: false),
                    http_status = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    records_count = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    result = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    error_message = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    requested_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    response_ms = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTEGRATION_LOG", x => x.id_log);
                    table.ForeignKey(
                        name: "fk_integration_log_city",
                        column: x => x.id_city,
                        principalTable: "CITY",
                        principalColumn: "id_city",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SENSOR",
                columns: table => new
                {
                    id_sensor = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    location = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    source = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    installed_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    last_reading_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSOR", x => x.id_sensor);
                    table.ForeignKey(
                        name: "fk_sensor_city",
                        column: x => x.id_city,
                        principalTable: "CITY",
                        principalColumn: "id_city",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    id_user = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    name = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    password_hash = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    phone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    is_active = table.Column<string>(type: "CHAR(1)", nullable: false),
                    notify_email = table.Column<string>(type: "CHAR(1)", nullable: false),
                    notify_push = table.Column<string>(type: "CHAR(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.id_user);
                    table.ForeignKey(
                        name: "fk_users_city",
                        column: x => x.id_city,
                        principalTable: "CITY",
                        principalColumn: "id_city",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AIR_READING",
                columns: table => new
                {
                    id_reading = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_sensor = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    pm25 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    pm10 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    co2 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    co = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    no2 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    so2 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    o3 = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    temperature = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    humidity = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    aqi = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    category = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    source = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    reading_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIR_READING", x => x.id_reading);
                    table.ForeignKey(
                        name: "fk_air_reading_city",
                        column: x => x.id_city,
                        principalTable: "CITY",
                        principalColumn: "id_city",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_air_reading_sensor",
                        column: x => x.id_sensor,
                        principalTable: "SENSOR",
                        principalColumn: "id_sensor",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ALERT_CONFIG",
                columns: table => new
                {
                    id_alert_config = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_user = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_city = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    pollutant = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    threshold = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    @operator = table.Column<string>(name: "operator", type: "CHAR(2)", nullable: false),
                    severity = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    is_active = table.Column<string>(type: "CHAR(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERT_CONFIG", x => x.id_alert_config);
                    table.ForeignKey(
                        name: "fk_alert_config_city",
                        column: x => x.id_city,
                        principalTable: "CITY",
                        principalColumn: "id_city",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_alert_config_user",
                        column: x => x.id_user,
                        principalTable: "USERS",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ALERT_EVENT",
                columns: table => new
                {
                    id_event = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_alert_config = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    id_reading = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    measured_value = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    message = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    notification_sent = table.Column<string>(type: "CHAR(1)", nullable: false),
                    event_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    notified_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERT_EVENT", x => x.id_event);
                    table.ForeignKey(
                        name: "fk_alert_event_config",
                        column: x => x.id_alert_config,
                        principalTable: "ALERT_CONFIG",
                        principalColumn: "id_alert_config",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_alert_event_reading",
                        column: x => x.id_reading,
                        principalTable: "AIR_READING",
                        principalColumn: "id_reading",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_air_reading_category",
                table: "AIR_READING",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "idx_air_reading_city_date",
                table: "AIR_READING",
                columns: new[] { "id_city", "reading_at" });

            migrationBuilder.CreateIndex(
                name: "idx_air_reading_date",
                table: "AIR_READING",
                column: "reading_at");

            migrationBuilder.CreateIndex(
                name: "idx_air_reading_sensor",
                table: "AIR_READING",
                column: "id_sensor");

            migrationBuilder.CreateIndex(
                name: "idx_alert_config_city",
                table: "ALERT_CONFIG",
                column: "id_city");

            migrationBuilder.CreateIndex(
                name: "idx_alert_config_user",
                table: "ALERT_CONFIG",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "uq_alert_config",
                table: "ALERT_CONFIG",
                columns: new[] { "id_user", "id_city", "pollutant", "operator" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_alert_event_config",
                table: "ALERT_EVENT",
                column: "id_alert_config");

            migrationBuilder.CreateIndex(
                name: "idx_alert_event_date",
                table: "ALERT_EVENT",
                column: "event_at");

            migrationBuilder.CreateIndex(
                name: "idx_alert_event_reading",
                table: "ALERT_EVENT",
                column: "id_reading");

            migrationBuilder.CreateIndex(
                name: "idx_alert_event_status",
                table: "ALERT_EVENT",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_city_country",
                table: "CITY",
                column: "id_country");

            migrationBuilder.CreateIndex(
                name: "idx_city_lat_lng",
                table: "CITY",
                columns: new[] { "latitude", "longitude" });

            migrationBuilder.CreateIndex(
                name: "uq_country_iso",
                table: "COUNTRY",
                column: "iso_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_integration_log_api",
                table: "INTEGRATION_LOG",
                columns: new[] { "api_name", "requested_at" });

            migrationBuilder.CreateIndex(
                name: "idx_integration_log_city",
                table: "INTEGRATION_LOG",
                column: "id_city");

            migrationBuilder.CreateIndex(
                name: "idx_integration_log_result",
                table: "INTEGRATION_LOG",
                column: "result");

            migrationBuilder.CreateIndex(
                name: "idx_sensor_city",
                table: "SENSOR",
                column: "id_city");

            migrationBuilder.CreateIndex(
                name: "idx_sensor_status",
                table: "SENSOR",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_users_city",
                table: "USERS",
                column: "id_city");

            migrationBuilder.CreateIndex(
                name: "uq_users_email",
                table: "USERS",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALERT_EVENT");

            migrationBuilder.DropTable(
                name: "INTEGRATION_LOG");

            migrationBuilder.DropTable(
                name: "ALERT_CONFIG");

            migrationBuilder.DropTable(
                name: "AIR_READING");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "SENSOR");

            migrationBuilder.DropTable(
                name: "CITY");

            migrationBuilder.DropTable(
                name: "COUNTRY");
        }
    }
}
