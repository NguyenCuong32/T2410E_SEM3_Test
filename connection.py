# connection.py
from pymongo import MongoClient

client = MongoClient("mongodb://127.0.0.1:27017/")

db = client["eShop"]

def test_connection():
    try:
        client.admin.command("ping")
        print(" Kết nối MongoDB thành công!")
    except Exception as e:
        print(" Kết nối MongoDB thất bại:", e)
