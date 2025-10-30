from flask import Flask, render_template, request, redirect, url_for
from pymongo import MongoClient

app = Flask(__name__)

# Kết nối MongoDB
client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
collection = db["OrderCollection"]

# -----------------------------
# TRANG CHÍNH - HIỂN THỊ ĐƠN HÀNG
# -----------------------------
@app.route("/")
def index():
    orders = list(collection.find())
    total_amount = 0
    total_somi = 0

    for order in orders:
        # Tính tổng tiền từng đơn
        order_total = sum(p["price"] * p["quantity"] for p in order["products"])
        order["total_amount"] = order_total
        total_amount += order_total
        # Đếm tổng sản phẩm "somi"
        total_somi += sum(1 for p in order["products"] if "somi" in p["product_id"])

    return render_template("index.html", orders=orders, total_amount=total_amount, total_somi=total_somi)

# -----------------------------
# THÊM ĐƠN HÀNG
# -----------------------------
@app.route("/add", methods=["GET", "POST"])
def add_order():
    if request.method == "POST":
        orderid = int(request.form["orderid"])
        product_id = request.form["product_id"]
        product_name = request.form["product_name"]
        size = request.form["size"]
        price = float(request.form["price"])
        quantity = int(request.form["quantity"])
        address = request.form["address"]

        new_order = {
            "orderid": orderid,
            "products": [
                {
                    "product_id": product_id,
                    "product_name": product_name,
                    "size": size,
                    "price": price,
                    "quantity": quantity
                }
            ],
            "total_amount": price * quantity,
            "delivery_address": address
        }

        collection.insert_one(new_order)
        return redirect(url_for("index"))

    return render_template("add_order.html")

# -----------------------------
# SỬA ĐỊA CHỈ GIAO HÀNG
# -----------------------------
@app.route("/edit/<int:orderid>", methods=["GET", "POST"])
def edit_order(orderid):
    order = collection.find_one({"orderid": orderid})
    if not order:
        return "Order not found!"

    if request.method == "POST":
        new_address = request.form["address"]
        collection.update_one({"orderid": orderid}, {"$set": {"delivery_address": new_address}})
        return redirect(url_for("index"))

    return render_template("edit_order.html", order=order)

# -----------------------------
# XÓA ĐƠN HÀNG
# -----------------------------
@app.route("/delete/<int:orderid>")
def delete_order(orderid):
    collection.delete_one({"orderid": orderid})
    return redirect(url_for("index"))

# -----------------------------
if __name__ == "__main__":
    app.run(debug=True)
