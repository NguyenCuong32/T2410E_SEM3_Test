from pymongo import MongoClient
from config import MONGO_URI, DATABASE_NAME, COLLECTION_NAME
from sample_data import SAMPLE_ORDERS

class OrderService:
    def __init__(self):
        self.client = MongoClient(MONGO_URI)
        self.db = self.client[DATABASE_NAME]
        self.collection = self.db[COLLECTION_NAME]

    def insert_sample_orders(self):
        try:
            self.collection.delete_many({}) 
            result = self.collection.insert_many(SAMPLE_ORDERS)
            return {"status": "success", "inserted_count": len(result.inserted_ids)}
        except Exception as e:
            return {"status": "error", "message": str(e)}

    def update_delivery_address(self, order_id: int, new_address: str):
        try:
            result = self.collection.update_one(
                {"orderid": order_id},
                {"$set": {"delivery_address": new_address}}
            )
            return {"status": "success", "matched_count": result.matched_count, "modified_count": result.modified_count}
        except Exception as e:
            return {"status": "error", "message": str(e)}

    def delete_order(self, order_id: int):
        try:
            result = self.collection.delete_one({"orderid": order_id})
            return {"status": "success", "deleted_count": result.deleted_count}
        except Exception as e:
            return {"status": "error", "message": str(e)}

    def get_all_products_details(self):
        try:
            pipeline = [
                {"$unwind": "$products"},
                {"$project": {
                    "_id": 0,
                    "product_name": "$products.product_name",
                    "price": "$products.price",
                    "quantity": "$products.quantity",
                    "total": {"$multiply": ["$products.price", "$products.quantity"]}
                }}
            ]
            products_list = list(self.collection.aggregate(pipeline))
            
            for i, product in enumerate(products_list):
                product['No'] = i + 1
            
            return {"status": "success", "data": products_list}
        except Exception as e:
            return {"status": "error", "message": str(e)}

    def calculate_total_amount(self):
        try:
            pipeline = [
                {"$group": {"_id": None, "total_sum": {"$sum": "$total_amount"}}}
            ]
            result = list(self.collection.aggregate(pipeline))
            
            if result:
                return {"status": "success", "total_amount": result[0]["total_sum"]}
            return {"status": "success", "total_amount": 0.0}
        except Exception as e:
            return {"status": "error", "message": str(e)}

    def count_somi_products(self):
        try:
            pipeline = [
                {"$unwind": "$products"},
                {"$match": {"products.product_id": "somi"}},
                {"$group": {"_id": None, "total_somi_count": {"$sum": "$products.quantity"}}}
            ]
            result = list(self.collection.aggregate(pipeline))
            count = result[0]["total_somi_count"] if result else 0
            return {"status": "success", "product_id": "somi", "total_quantity": count}
        except Exception as e:
            return {"status": "error", "message": str(e)}