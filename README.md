# OnlineShop Server/Client

# Description:
**A shopping solution that has both client side and server side using Sockets from .NET Framework, Multithreading, and Asynchronous programming concepts**

# Server Side:
- A console application, which it prints each response to the output window before sending back to the client side
- When it first runs, five per-defined products with random quantities (1-3) are initialized. Hence, products are the same every time the server starts, and the quantities for each product is randomized.
- Three accounts are created, each account has an account number and a user name.
- When the server is active, it stores ordering information to **ConcurrentDictionary**. And, the server does not store the ordering information to the local system, so once the server is shutdown, all information are disposed.
- Server sends back appropriate response depending on the client’s command. (The list of protocols is below)
- The handler code for the server is in separate class.
- The server is capable to establish communication with multiple clients at the same time.

# Client Side:
- The client-side GUI is implemented using Windows Forms application, with an appropriate user interface. The user can select one product, then make the purchase. (quantity as one).
- When it first opens, a login form appears with two input fields: hostname/IP and the account number. (localhost as the default value for the hostname)
- If the server is not available, the application displays an error message; if the server is available/active but login failed, a different error message gets displayed.
- Once the user successfully connects to the server, the application gets all products information (names and quantities), then shows all information on the GUI.
- The user has the capability of disconnecting from the server gracefully. Upon disconnecting, the application closes. The application gracefully disconnect when the form is closed.
- When a product is not available during the process of user making a purchase, the application displays an appropriate message stating that the product is no longer available.
- The application also has the capability of showing the current purchase orders.
- The server handler code is in a separate class from the user interface.
- The application ensures that code that writes to or reads from the server does not block the GUI thread using Multithreading and Asynchronous programming.

# Protocol Standards for this Project:
<table>
	<tr>
		<th align="left">Client Commands</th>
    	<th align="left">Server Response</th>
  	</tr>
  	<tr>
    	<td>DISCONNECT</td>
    	<td>No response.<br><em>The server removes the client from the list of active clients. Both sides end the connection.</em></br></td>
  	</tr>
	<tr>
    	<td>CONNECT:account_no</td>
    	<td>CONNECTED:user_name<br><em>The client has successfully connected with the specified account number. The server returns the connected client's name.</em></br>CONNECT_ERROR<br><em>The client's connection attempt is unsuccessful. The account_no is not valid.</em></br></td>
	</tr>	
	<tr>
    	<td>GET_PRODUCTS</td>
    	<td>PRODUCTS:product_name1,quantity1|product_name2,quantity2|…<br><em>The server sends all product information (e.g. PRODUCTS:APPLE,2|ORANGE,1).</em></br></td>
	</tr>
	<tr>
    	<td>GET_ORDERS</td>
    	<td>ORDERS:product_name1,quantity1,user_name|product_name2,quantity2, user_name|…<br><em>The server sends the purchase orders of all clients. (e.g. ORDERS:APPLE,1,John|ORANGE,1,Doe).</em></br></td>
	</tr>
	<tr>
    	<td>PURCHASE:product_name</td>
    	<td>DONE<br><em>The order successfully placed</em></br>NOT_AVAILABLE<br><em>The product is not available (i.e., is already purchased by another client) and cannot be purchase.</em></br>NOT_VALID<br><em>The specified product is not valid.</em></br></td>
	</tr>
</table>

# Demo for the solution:
https://github.com/YRZiTO/OnlineShop-Client-Server/assets/106015329/fb437d43-082f-40f6-b20c-4bcc2ee08e7a


