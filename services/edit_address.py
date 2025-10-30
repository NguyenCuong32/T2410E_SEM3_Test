from db.connection import get_database

def edit_delivery_address(orderid, new_address):
    db = get_database()
    collection = db["OrderCollection"]
    collection.update_one({"orderid": orderid}, {"$set": {"delivery_address": new_address}})
    print(f"✅ Updated delivery address for orderid {orderid} to '{new_address}'.")
# câu 3