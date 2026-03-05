const { app } = require('@azure/functions');
const pool = require('../../db');

app.http('registerplayer', {
    methods: ['POST'],
    authLevel: 'anonymous',

    handler: async (request, context) => {

        try {

            const body = await request.json();

            const { playerName, fullName, age, level } = body;

            await pool.query(
                "INSERT INTO Player (PlayerName, FullName, Age, Level) VALUES (?, ?, ?, ?)",
                [playerName, fullName, age, level]
            );

            return {
                status: 200,
                jsonBody: { message: "Player registered" }
            };

        } catch (error) {

            return {
                status: 500,
                jsonBody: { error: error.message }
            };

        }
    }
});