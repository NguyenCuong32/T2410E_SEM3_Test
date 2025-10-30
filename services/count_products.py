from db.connection import get_database

def count_product_id(product_id):
    db = get_database()
    collection = db["OrderCollection"]
    count = 0
    for order in collection.find():
        for p in order["products"]:
            if p["product_id"] == product_id:
                count += 1
    print(f"🔍 Total number of product_id = '{product_id}': {count}")
# câu 7