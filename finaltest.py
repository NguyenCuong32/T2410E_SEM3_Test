from pymongo import MongoClient
import pprint 
try:
    client = MongoClient('mongodb://localhost:27017/')
    db = client['eShop']
    collection = db['OrderCollection']
    print("--- 1. Connection Successful ---")
    print(f"Connected to Database: '{db.name}', Collection: '{collection.name}'")
    collection.delete_many({})
    print("Cleared collection for a fresh start.")
    order1 = {
        "orderid": 1,
        "products": [
            {
                "product_id": "quanau",
                "product_name": "quan au",
                "size": "XL",
                "price": 10,
                "quantity": 1
            },
            {
                "product_id": "somi",
                "product_name": "ao so mi",
                "size": "XL",
                "price": 10.5,
                "quantity": 2
            }
        ],
        "total_amount": 31,
        "delivery_address": "Hanoi"
    }

    order2 = {
        "orderid": 2,
        "products": [
            {
                "product_id": "giay",
                "product_name": "giay da",
                "size": "42",
                "price": 50,
                "quantity": 1
            }
        ],
        "total_amount": 50,
        "delivery_address": "Da Nang"
    }
    orders_to_insert = [order1, order2]
    insert_result = collection.insert_many(orders_to_insert)
    print(f"\n--- 2. Inserted {len(insert_result.inserted_ids)} documents ---")
    query_filter = {"orderid": 1}
    update_operation = {"$set": {"delivery_address": "Ho Chi Minh City"}}
    update_result = collection.update_one(query_filter, update_operation)
    print(f"Documents matched: {update_result.matched_count}")
    print(f"Documents modified: {update_result.modified_count}")
    delete_filter = {"orderid": 2}
    delete_result = collection.delete_one(delete_filter)
    print(f"Documents deleted: {delete_result.deleted_count}")
    all_orders = collection.find()
    
    count = collection.count_documents({})
    print(f"Found {count} document(s):")
    
    for order in all_orders:
        pprint.pprint(order)
except Exception as e:
    print(f"An error occurred: {e}")
finally:
    if 'client' in locals():
        client.close()
        print("\nconnection close.")