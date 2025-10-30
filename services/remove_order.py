from db.connection import get_database

def remove_order(orderid):
    db = get_database()
    collection = db["OrderCollection"]
    collection.delete_one({"orderid": orderid})
    print(f"🗑️ Removed order with orderid {orderid}.")
# câu 4