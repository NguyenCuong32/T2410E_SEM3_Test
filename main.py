from order_service import get_all_orders, find_order_by_id, update_order_address, delete_order
from connection import test_connection

def show_menu():
    print("\n=== QUẢN LÝ ĐƠN HÀNG eShop ===")
    print("1. Xem tất cả đơn hàng")
    print("2. Tìm đơn hàng theo ID")
    print("3. Cập nhật địa chỉ giao hàng")
    print("4. Xóa đơn hàng")
    print("5. Thoát")

def main():
    test_connection()
    while True:
        show_menu()
        choice = input("Chọn chức năng (1-5): ")

        if choice == "1":
            orders = get_all_orders()
            for order in orders:
                print(f"\nMã đơn: {order['orderid']}")
                print(f"Địa chỉ: {order.get('delivery_address', 'Không có')}")
                print("Sản phẩm:")
                for p in order["products"]:
                    print(f" - {p['product_name']} ({p['size']}), giá {p['price']}, SL: {p['quantity']}")
                print(f"Tổng tiền: {order['total_amount']}")
        
        elif choice == "2":
            orderid = int(input("Nhập mã đơn cần tìm: "))
            order = find_order_by_id(orderid)
            if order:
                print(order)
            else:
                print("❌ Không tìm thấy đơn hàng.")

        elif choice == "3":
            orderid = int(input("Nhập mã đơn cần cập nhật: "))
            new_address = input("Nhập địa chỉ mới: ")
            if update_order_address(orderid, new_address):
                print("✅ Cập nhật thành công!")
            else:
                print("❌ Không tìm thấy đơn hàng hoặc lỗi khi cập nhật.")

        elif choice == "4":
            orderid = int(input("Nhập mã đơn cần xóa: "))
            if delete_order(orderid):
                print("🗑️ Đã xóa đơn hàng thành công.")
            else:
                print("❌ Không tìm thấy đơn hàng.")

        elif choice == "5":
            print("Thoát chương trình.")
            break
        else:
            print("Lựa chọn không hợp lệ!")

if __name__ == "__main__":
    main()
