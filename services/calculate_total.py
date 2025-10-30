from db.connection import get_database

def calculate_total_amount():
    db = get_database()
    collection = db["OrderCollection"]
    pipeline = [{"$group": {"_id": None, "total_sum": {"$sum": "$total_amount"}}}]
    result = list(collection.aggregate(pipeline))
    if result:
        print("💵 Total amount of all orders:", result[0]["total_sum"])
    else:
        print("No data found.")
# câu 6