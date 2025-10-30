package org.example;

import com.mongodb.ConnectionString;
import com.mongodb.MongoClientSettings;
import com.mongodb.client.*;
import com.mongodb.client.model.Filters;
import com.mongodb.client.model.Updates;
import org.bson.Document;

import java.util.List;

public class Main {
    public static void main(String[] args) {
        String uri = "mongodb+srv://baochau:ldbc12345@cluster0.jrf1qww.mongodb.net/";

        ConnectionString connectionString = new ConnectionString(uri);
        MongoClientSettings settings = MongoClientSettings.builder()
                .applyConnectionString(connectionString)
                .build();

        try (MongoClient mongoClient = MongoClients.create(settings)) {

            MongoDatabase database = mongoClient.getDatabase("eShop");
            MongoCollection<Document> collection = database.getCollection("OrderCollection");
            System.out.println("✅ Connected to MongoDB");

            Document order1 = new Document("orderid", 1)
                    .append("products", List.of(
                            new Document("product_id", "quanau")
                                    .append("product_name", "quan au")
                                    .append("size", "XL")
                                    .append("price", 10)
                                    .append("quantity", 1),
                            new Document("product_id", "somi")
                                    .append("product_name", "ao so mi")
                                    .append("size", "XL")
                                    .append("price", 10.5)
                                    .append("quantity", 2)
                    ))
                    .append("total_amount", 31)
                    .append("delivery_address", "Hanoi");

            Document order2 = new Document("orderid", 2)
                    .append("products", List.of(
                            new Document("product_id", "jean")
                                    .append("product_name", "quan jean")
                                    .append("size", "L")
                                    .append("price", 15)
                                    .append("quantity", 1)
                    ))
                    .append("total_amount", 15)
                    .append("delivery_address", "Danang");

            collection.insertMany(List.of(order1, order2));
            System.out.println("✅ Inserted sample orders.");

            collection.updateOne(Filters.eq("orderid", 1),
                    Updates.set("delivery_address", "Ho Chi Minh City"));
            System.out.println("✅ Updated delivery_address for orderid = 1");

            collection.deleteOne(Filters.eq("orderid", 2));
            System.out.println("✅ Deleted order with orderid = 2");

            System.out.println("\n📦 All Orders:");
            for (Document orderDoc : collection.find()) {
                int orderId = orderDoc.getInteger("orderid");
                String address = orderDoc.getString("delivery_address");
                List<Document> products = orderDoc.getList("products", Document.class);

                System.out.println("Order ID: " + orderId + " | Address: " + address);
                System.out.println("No\tProduct name\tPrice\tQuantity\tTotal");

                int index = 1;
                for (Document p : products) {
                    double price = ((Number) p.get("price")).doubleValue();
                    int quantity = ((Number) p.get("quantity")).intValue();
                    double total = price * quantity;
                    System.out.printf("%d\t%s\t\t%.2f\t%d\t\t%.2f%n",
                            index++, p.getString("product_name"), price, quantity, total);
                }
                System.out.println();
            }

            Document order = collection.find(Filters.eq("orderid", 1)).first();
            if (order != null) {
                List<Document> products = order.getList("products", Document.class);
                double totalAmount = 0;
                for (Document p : products) {
                    double price = ((Number) p.get("price")).doubleValue();
                    int quantity = ((Number) p.get("quantity")).intValue();
                    totalAmount += price * quantity;
                }
                System.out.println("💰 Calculated total amount for orderid 1 = " + totalAmount);
            }

            long count = collection.countDocuments(Filters.eq("products.product_id", "somi"));
            System.out.println("📊 Count of product_id = 'somi': " + count);
        }
    }
}
