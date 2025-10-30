package org.example;

import com.mongodb.ConnectionString;
import com.mongodb.MongoClientSettings;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;

public class Main {
    public static void main(String[] args) {
        String uri = "mongodb+srv://baochau:ldbc12345@cluster0.jrf1qww.mongodb.net/";

        ConnectionString connectionString = new ConnectionString(uri);
        MongoClientSettings settings = MongoClientSettings.builder()
                .applyConnectionString(connectionString)
                .build();
        try (MongoClient mongoClient = MongoClients.create(settings)) {

            MongoDatabase database = mongoClient.getDatabase("large_data_db");

            MongoCollection<Document> collection = database.getCollection("students");

            Document doc = new Document("name", "Alice")
                    .append("age", 21)
                    .append("major", "Computer Science");
            collection.insertOne(doc);
            System.out.println("✅ Inserted: " + doc.toJson());

            for (Document d : collection.find()) {
                System.out.println("📄 " + d.toJson());
            }
        }
    }
}
