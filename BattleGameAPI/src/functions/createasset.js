const { app } = require('@azure/functions');
const pool = require('../../db');

app.http('createasset', {
    methods: ['POST'],
    authLevel: 'anonymous',

    handler: async (request, context) => {

        const body = await request.json();
        const { assetName, assetType } = body;

        try {

            const [result] = await pool.query(
                "INSERT INTO Asset (AssetName, AssetType) VALUES (?, ?)",
                [assetName, assetType]
            );

            return {
                status: 200,
                jsonBody: {
                    message: "Asset created",
                    assetId: result.insertId
                }
            };

        } catch (error) {

            return {
                status: 500,
                body: error.message
            };
        }
    }
});