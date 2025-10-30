

from pymongo import MongoClient
from tabulate import tabulate
from datetime import datetime

class OrderManagementSystem:
    """
    Class quản lý hệ thống đơn hàng E-commerce
    """
    
    def __init__(self, connection_string="mongodb://localhost:27017/"):
        """
        BƯỚC 1: Kết nối MongoDB
        
        Args:
            connection_string: Chuỗi kết nối đến MongoDB server
        """
        try:
            print("\n" + "="*70)
            print("BƯỚC 1: KẾT NỐI MONGODB")
            print("="*70)
            
            # Kết nối MongoDB
            self.client = MongoClient(connection_string)
            
            # Chọn database và collection
            self.db = self.client['eShop']  # Tên database
            self.collection = self.db['OrderCollection']  # Tên collection
            
            # Test connection
            self.client.server_info()
            
            print(f"✓ Kết nối thành công!")
            print(f"✓ Connection String: {connection_string}")
            print(f"✓ Database: {self.db.name}")
            print(f"✓ Collection: {self.collection.name}")
            
        except Exception as e:
            print(f"✗ LỖI kết nối MongoDB: {e}")
            print("\nVui lòng kiểm tra:")
            print("  1. MongoDB Server có đang chạy không?")
            print("  2. Connection string có đúng không?")
            raise
    
    def insert_sample_orders(self):
        """
        BƯỚC 2: Thêm nhiều đơn hàng vào OrderCollection
        """
        print("\n" + "="*70)
        print("BƯỚC 2: THÊM NHIỀU ĐƠN HÀNG VÀO DATABASE")
        print("="*70)
        
        # Dữ liệu đơn hàng mẫu theo đề bài
        sample_orders = [
            {
                "orderid": 1,
                "products": [
                    {
                        "product_id": "quanau",
                        "product_name": "quan au",
                        "size": "XL",
                        "price": 10,
                        "quantity": 1
                    },
                    {
                        "product_id": "somi",
                        "product_name": "ao so mi",
                        "size": "XL",
                        "price": 10.5,
                        "quantity": 2
                    }
                ],
                "total_amount": 31,
                "delivery_address": "Hanoi"
            },
            {
                "orderid": 2,
                "products": [
                    {
                        "product_id": "somi",
                        "product_name": "ao so mi",
                        "size": "L",
                        "price": 10.5,
                        "quantity": 3
                    }
                ],
                "total_amount": 31.5,
                "delivery_address": "Ho Chi Minh"
            },
            {
                "orderid": 3,
                "products": [
                    {
                        "product_id": "quanjean",
                        "product_name": "quan jean",
                        "size": "M",
                        "price": 15,
                        "quantity": 1
                    },
                    {
                        "product_id": "somi",
                        "product_name": "ao so mi",
                        "size": "XL",
                        "price": 10.5,
                        "quantity": 1
                    }
                ],
                "total_amount": 25.5,
                "delivery_address": "Da Nang"
            }
        ]
        
        try:
            # Xóa dữ liệu cũ (nếu có) để tránh trùng lặp khi test nhiều lần
            deleted_count = self.collection.delete_many({}).deleted_count
            if deleted_count > 0:
                print(f"✓ Đã xóa {deleted_count} đơn hàng cũ")
            
            # Insert nhiều documents cùng lúc
            result = self.collection.insert_many(sample_orders)
            
            print(f"✓ Đã thêm {len(result.inserted_ids)} đơn hàng vào database")
            print(f"\nChi tiết:")
            for i, order in enumerate(sample_orders, 1):
                print(f"  - Đơn hàng #{order['orderid']}: "
                      f"{len(order['products'])} sản phẩm, "
                      f"tổng: {order['total_amount']}, "
                      f"giao đến: {order['delivery_address']}")
            
            return True
            
        except Exception as e:
            print(f"✗ LỖI khi thêm đơn hàng: {e}")
            return False
    
    def edit_delivery_address(self, orderid, new_address):
        """
        BƯỚC 3: Sửa địa chỉ giao hàng theo orderid
        
        Args:
            orderid: ID của đơn hàng cần sửa
            new_address: Địa chỉ mới
        """
        print("\n" + "="*70)
        print("BƯỚC 3: SỬA ĐỊA CHỈ GIAO HÀNG")
        print("="*70)
        
        try:
            # Lấy địa chỉ cũ trước
            old_order = self.collection.find_one({"orderid": orderid})
            
            if not old_order:
                print(f"✗ Không tìm thấy đơn hàng #{orderid}")
                return False
            
            old_address = old_order.get('delivery_address', 'N/A')
            
            # Cập nhật địa chỉ mới
            result = self.collection.update_one(
                {"orderid": orderid},  # Filter: tìm theo orderid
                {"$set": {"delivery_address": new_address}}  # Update: set địa chỉ mới
            )
            
            if result.modified_count > 0:
                print(f"✓ Đã cập nhật địa chỉ cho đơn hàng #{orderid}")
                print(f"  - Địa chỉ cũ: {old_address}")
                print(f"  - Địa chỉ mới: {new_address}")
                return True
            else:
                print(f"⚠ Đơn hàng #{orderid} đã có địa chỉ này rồi (không thay đổi)")
                return False
                
        except Exception as e:
            print(f"✗ LỖI khi sửa địa chỉ: {e}")
            return False
    
    def remove_order(self, orderid):
        """
        BƯỚC 4: Xóa đơn hàng theo orderid
        
        Args:
            orderid: ID của đơn hàng cần xóa
        """
        print("\n" + "="*70)
        print("BƯỚC 4: XÓA ĐƠN HÀNG")
        print("="*70)
        
        try:
            # Lấy thông tin đơn hàng trước khi xóa
            order = self.collection.find_one({"orderid": orderid})
            
            if not order:
                print(f"✗ Không tìm thấy đơn hàng #{orderid}")
                return False
            
            # Xóa document
            result = self.collection.delete_one({"orderid": orderid})
            
            if result.deleted_count > 0:
                print(f"✓ Đã xóa đơn hàng #{orderid}")
                print(f"  - Địa chỉ: {order.get('delivery_address', 'N/A')}")
                print(f"  - Tổng tiền: {order.get('total_amount', 0)}")
                return True
            else:
                print(f"✗ Không thể xóa đơn hàng #{orderid}")
                return False
                
        except Exception as e:
            print(f"✗ LỖI khi xóa đơn hàng: {e}")
            return False
    
    def display_all_orders_table(self):
        """
        BƯỚC 5: Hiển thị tất cả đơn hàng dạng bảng
        Format giống đề bài:
        +----+--------------+-------+----------+-------+
        | No | Product name | Price | Quantity | Total |
        +----+--------------+-------+----------+-------+
        """
        print("\n" + "="*70)
        print("BƯỚC 5: HIỂN THỊ TẤT CẢ ĐƠN HÀNG DẠNG BẢNG")
        print("="*70)
        
        try:
            # Lấy tất cả đơn hàng từ database
            orders = list(self.collection.find({}))
            
            if not orders:
                print("✗ Không có đơn hàng nào trong database")
                return None
            
            print(f"✓ Tìm thấy {len(orders)} đơn hàng\n")
            
            # Chuẩn bị dữ liệu cho bảng
            table_data = []
            row_number = 1
            
            # Duyệt qua từng đơn hàng
            for order in orders:
                products = order.get('products', [])
                
                # Duyệt qua từng sản phẩm trong đơn hàng
                for product in products:
                    price = product.get('price', 0)
                    quantity = product.get('quantity', 0)
                    total = price * quantity
                    
                    table_data.append([
                        row_number,
                        product.get('product_name', 'N/A'),
                        price,
                        quantity,
                        total
                    ])
                    row_number += 1
            
            # Hiển thị bảng với tabulate
            headers = ["No", "Product name", "Price", "Quantity", "Total"]
            print(tabulate(table_data, headers=headers, tablefmt="grid"))
            
            print(f"\n✓ Tổng số dòng: {len(table_data)}")
            
            return table_data
            
        except Exception as e:
            print(f"✗ LỖI khi hiển thị đơn hàng: {e}")
            return None
    
    def calculate_total_amount(self):
        """
        BƯỚC 6: Tính tổng tiền của tất cả đơn hàng
        """
        print("\n" + "="*70)
        print("BƯỚC 6: TÍNH TỔNG TIỀN TẤT CẢ ĐƠN HÀNG")
        print("="*70)
        
        try:
            # Sử dụng Aggregation Pipeline để tính tổng
            pipeline = [
                {
                    "$group": {
                        "_id": None,
                        "total": {"$sum": "$total_amount"}
                    }
                }
            ]
            
            result = list(self.collection.aggregate(pipeline))
            
            if result:
                total = result[0]['total']
                print(f"✓ Tổng tiền tất cả đơn hàng: {total}")
                
                # Hiển thị chi tiết từng đơn hàng
                orders = list(self.collection.find({}, {"orderid": 1, "total_amount": 1}))
                print(f"\nChi tiết:")
                for order in orders:
                    print(f"  - Đơn hàng #{order['orderid']}: {order['total_amount']}")
                
                return total
            else:
                print("✓ Tổng tiền: 0 (không có đơn hàng)")
                return 0
                
        except Exception as e:
            print(f"✗ LỖI khi tính tổng tiền: {e}")
            return None
    
    def count_somi_products(self):
        """
        BƯỚC 7: Đếm tổng số lượng sản phẩm có product_id = "somi"
        """
        print("\n" + "="*70)
        print("BƯỚC 7: ĐẾM SẢN PHẨM 'SOMI'")
        print("="*70)
        
        try:
            # Sử dụng Aggregation Pipeline
            pipeline = [
                # Stage 1: Tách mảng products thành nhiều documents
                {"$unwind": "$products"},
                
                # Stage 2: Lọc chỉ lấy sản phẩm có product_id = "somi"
                {
                    "$match": {
                        "products.product_id": "somi"
                    }
                },
                
                # Stage 3: Nhóm và tính tổng quantity
                {
                    "$group": {
                        "_id": None,
                        "total_quantity": {"$sum": "$products.quantity"}
                    }
                }
            ]
            
            result = list(self.collection.aggregate(pipeline))
            
            if result:
                count = result[0]['total_quantity']
                print(f"✓ Tổng số sản phẩm 'somi': {count}")
                
                # Hiển thị chi tiết từng đơn hàng có somi
                detail_pipeline = [
                    {"$unwind": "$products"},
                    {
                        "$match": {
                            "products.product_id": "somi"
                        }
                    },
                    {
                        "$project": {
                            "orderid": 1,
                            "product_name": "$products.product_name",
                            "quantity": "$products.quantity"
                        }
                    }
                ]
                
                details = list(self.collection.aggregate(detail_pipeline))
                print(f"\nChi tiết:")
                for detail in details:
                    print(f"  - Đơn hàng #{detail['orderid']}: "
                          f"{detail['quantity']} sản phẩm '{detail['product_name']}'")
                
                return count
            else:
                print("✓ Không tìm thấy sản phẩm 'somi' nào")
                return 0
                
        except Exception as e:
            print(f"✗ LỖI khi đếm sản phẩm: {e}")
            return None
    
    def run_all_tasks(self):
        """
        Chạy tất cả các bước theo yêu cầu đề bài
        """
        print("\n")
        print("╔" + "="*68 + "╗")
        print("║" + " "*15 + "HỆ THỐNG QUẢN LÝ ĐƠN HÀNG E-COMMERCE" + " "*16 + "║")
        print("║" + " "*20 + "MongoDB + Python + PyMongo" + " "*22 + "║")
        print("╚" + "="*68 + "╝")
        
        # BƯỚC 1: Đã thực hiện trong __init__
        
        # BƯỚC 2: Thêm đơn hàng mẫu
        self.insert_sample_orders()
        
        # BƯỚC 3: Sửa địa chỉ giao hàng của đơn hàng #1
        self.edit_delivery_address(1, "Hanoi - Updated")
        
        # BƯỚC 4: Xóa đơn hàng
        # Uncomment dòng dưới nếu muốn xóa đơn hàng #2
        # self.remove_order(2)
        print("\n" + "="*70)
        print("BƯỚC 4: XÓA ĐƠN HÀNG")
        print("="*70)
        print("⚠ Bước này đã được comment để giữ dữ liệu đầy đủ")
        print("  Uncomment dòng 'self.remove_order(2)' trong code nếu muốn xóa")
        
        # BƯỚC 5: Hiển thị tất cả đơn hàng dạng bảng
        self.display_all_orders_table()
        
        # BƯỚC 6: Tính tổng tiền
        self.calculate_total_amount()
        
        # BƯỚC 7: Đếm sản phẩm "somi"
        self.count_somi_products()
        
        # Kết thúc
        print("\n" + "="*70)
        print("✓ HOÀN THÀNH TẤT CẢ CÁC YÊU CẦU!")
        print("="*70)
        print("\nBây giờ hãy mở MongoDB Compass để xem kết quả!")
        print(f"  1. Mở MongoDB Compass")
        print(f"  2. Kết nối: mongodb://localhost:27017")
        print(f"  3. Vào database: eShop")
        print(f"  4. Xem collection: OrderCollection")
    
    def close_connection(self):
        """
        Đóng kết nối MongoDB
        """
        self.client.close()
        print("\n✓ Đã đóng kết nối MongoDB")


# =============================================================================
# CHƯƠNG TRÌNH CHÍNH
# =============================================================================
if __name__ == "__main__":
    try:
        print("\n" + "="*70)
        print("KHỞI ĐỘNG CHƯƠNG TRÌNH...")
        print("="*70)
        
        # Khởi tạo hệ thống
        # Nếu MongoDB có username/password, thay đổi connection string:
        # system = OrderManagementSystem("mongodb://username:password@localhost:27017/")
        system = OrderManagementSystem("mongodb://localhost:27017/")
        
        # Chạy tất cả các bước
        system.run_all_tasks()
        
        # Đóng kết nối
        system.close_connection()
        
        print("\n" + "="*70)
        print("CHƯƠNG TRÌNH HOÀN TẤT!")
        print("="*70)
        
    except KeyboardInterrupt:
        print("\n\n⚠ Chương trình bị gián đoạn bởi người dùng (Ctrl+C)")
        
    except Exception as e:
        print("\n" + "="*70)
        print("✗ LỖI NGHIÊM TRỌNG!")
        print("="*70)
        print(f"Lỗi: {e}")
        print("\nVui lòng kiểm tra:")
        print("  1. MongoDB Server có đang chạy không?")
        print("  2. Đã cài đặt pymongo và tabulate chưa?")
        print("  3. Connection string có đúng không?")
        print("\nCách khắc phục:")
        print("  - Kiểm tra MongoDB: services.msc (Windows)")
        print("  - Cài thư viện: pip install pymongo tabulate")