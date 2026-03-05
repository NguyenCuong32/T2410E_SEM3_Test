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
    context.log('createasset function triggered');

    try {
        // Validate required fields
        const { assetName, levelRequire } = req.body;

        if (!assetName) {
            context.res = {
                status: 400,
                body: {
                    success: false,
                    message: 'Missing required field: assetName'
                }
            };
            return;
        }

        // Connect to database
        await sql.connect(config);

        // Insert new asset with UNIQUEIDENTIFIER
        const insertRequest = new sql.Request();
        const result = await insertRequest
            .input('assetName', sql.NVarChar, assetName)
            .input('levelRequire', sql.Int, levelRequire || 1)
            .query(`
                INSERT INTO Asset (AssetName, LevelRequire)
                VALUES (@assetName, @levelRequire);
                SELECT AssetId FROM Asset WHERE AssetName = @assetName;
            `);

        context.res = {
            status: 201,
            body: {
                success: true,
                message: 'Asset created successfully',
                assetId: result.recordset[0].AssetId
            }
        };

    } catch (error) {
        context.log.error('Error creating asset:', error);
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
