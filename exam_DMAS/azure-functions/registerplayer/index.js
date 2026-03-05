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
    context.log('registerplayer function triggered');

    try {
        // Validate required fields
        const { playerName, fullName, age, level, email } = req.body;

        if (!playerName) {
            context.res = {
                status: 400,
                body: {
                    success: false,
                    message: 'Missing required field: playerName'
                }
            };
            return;
        }

        // Connect to database
        await sql.connect(config);

        // Check if player already exists
        const checkRequest = new sql.Request();
        const existingPlayer = await checkRequest.input('playerName', sql.NVarChar, playerName)
            .query('SELECT PlayerId FROM Player WHERE PlayerName = @playerName');

        if (existingPlayer.recordset.length > 0) {
            context.res = {
                status: 409,
                body: {
                    success: false,
                    message: 'Player with this name already exists'
                }
            };
            return;
        }

        // Insert new player with UNIQUEIDENTIFIER
        const insertRequest = new sql.Request();
        const result = await insertRequest
            .input('playerName', sql.NVarChar, playerName)
            .input('fullName', sql.NVarChar, fullName || null)
            .input('age', sql.NVarChar, age ? age.toString() : null)
            .input('level', sql.Int, level || 1)
            .input('email', sql.NVarChar, email || null)
            .query(`
                INSERT INTO Player (PlayerName, FullName, Age, [Level], Email)
                VALUES (@playerName, @fullName, @age, @level, @email);
                SELECT PlayerId FROM Player WHERE PlayerName = @playerName;
            `);

        context.res = {
            status: 201,
            body: {
                success: true,
                message: 'Player registered successfully',
                playerId: result.recordset[0].PlayerId
            }
        };

    } catch (error) {
        context.log.error('Error registering player:', error);
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
