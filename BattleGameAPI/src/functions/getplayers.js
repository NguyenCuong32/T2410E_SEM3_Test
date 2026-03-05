const { app } = require('@azure/functions');
const pool = require('../../db');

app.http('getplayers', {
    methods: ['GET'],
    authLevel: 'anonymous',

    handler: async (request, context) => {

        try {

            const [rows] = await pool.query(
                "SELECT PlayerId, PlayerName FROM Player"
            );

            return {
                status: 200,
                jsonBody: rows
            };

        } catch (error) {

            return {
                status: 500,
                body: error.message
            };
        }
    }
});