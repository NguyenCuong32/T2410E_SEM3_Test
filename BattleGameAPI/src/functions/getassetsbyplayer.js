const { app } = require('@azure/functions');
const pool = require('../../db');

app.http('getassetsbyplayer', {
    methods: ['GET'],
    authLevel: 'anonymous',

    handler: async (request, context) => {

        try {

            const [rows] = await pool.query(`
                SELECT 
                p.PlayerName,
                p.Level,
                p.Age,
                a.AssetName
                FROM Player p
                JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
                JOIN Asset a ON pa.AssetId = a.AssetId
            `);

            return {
                status: 200,
                jsonBody: rows
            };

        } catch (error) {

            return {
                status: 500,
                jsonBody: { error: error.message }
            };

        }
    }
});