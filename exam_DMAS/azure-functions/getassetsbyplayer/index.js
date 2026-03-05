const sql = require('mssql');

// Database configuration - Replace with your actual Azure SQL Database connection string
const config = {
    server: process.env.DB_SERVER || 'your-server.database.windows.net',
    database: process.env.DB_NAME || 'BATTLEGAME',
    user: process.env.DB_USER || 'your-username',
    password: process.env.DB_PASSWORD || 'your-password',
    options: {
        encrypt: false,
        trustServerCertificate: true,
        enableArithAbort: true
    },
    pool: {
        max: 10,
        min: 0,
        idleTimeoutMillis: 30000
    }
};

module.exports = async function (context, req) {
    context.log('getassetsbyplayer function triggered');

    try {
        // Connect to database
        await sql.connect(config);

        // Call stored procedure to get assets by player
        const request = new sql.Request();
        const result = await request.execute('GetAssetsByPlayer');

        context.res = {
            status: 200,
            body: {
                success: true,
                data: result.recordset
            }
        };

    } catch (error) {
        context.log.error('Error getting assets by player:', error);
        context.res = {
            status: 500,
            body: {
                success: false,
                message: 'Internal server error',
                error: error.message
            }
        };
    } finally {
        await sql.close();
    }
};
