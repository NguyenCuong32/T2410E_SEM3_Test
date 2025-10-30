from pymongo import MongoClient

def get_database():
    """Kết nối tới MongoDB và trả về database eShop"""
    client = MongoClient("mongodb://localhost:27017/")
    db = client["eShop"]
    return db
#  câu 1