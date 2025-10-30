from connection import db

orders = db["OrderCollection"]

# Lấy tất cả đơn hàng
def get_all_orders():
    return list(orders.find())

# Tìm đơn hàng theo mã
def find_order_by_id(orderid):
    return orders.find_one({"orderid": orderid})

# Cập nhật địa chỉ giao hàng
def update_order_address(orderid, new_address):
    result = orders.update_one(
        {"orderid": orderid},
        {"$set": {"delivery_address": new_address}}
    )
    return result.modified_count > 0

# Xóa đơn hàng
def delete_order(orderid):
    result = orders.delete_one({"orderid": orderid})
    return result.deleted_count > 0
