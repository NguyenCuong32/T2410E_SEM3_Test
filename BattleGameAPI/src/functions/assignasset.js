const { app } = require('@azure/functions');
const pool = require('../../db');

app.http('assignasset', {
    methods: ['POST'],
    authLevel: 'anonymous',

    handler: async (request, context) => {

        const body = await request.json();
        const { playerId, assetId } = body;

        try {

            await pool.query(
                "INSERT INTO PlayerAsset (PlayerId, AssetId) VALUES (?, ?)",
                [playerId, assetId]
            );

            return {
                status: 200,
                body: "Asset linked to player"
            };

        } catch (error) {

            return {
                status: 500,
                body: error.message
            };
        }
    }
});