SELECT *
FROM Customers
WHERE CustomerName LIKE '%SSON'
ORDER BY CustomerName DESC


SELECT *
FROM HotelRooms
WHERE RoomNumber LIKE '%01'
ORDER BY RoomNumber DESC


SELECT CheckInDate, FORMAT(CheckInDate, 'yyyy-MM') AS DateForCheckIn
FROM Bookings
WHERE CheckInDate = '2025-03'
ORDER BY BookingsId DESC
