async function loadProducts() {

    const status = document.getElementById("status");

    try {

        const response = await fetch("/api/products");

        const products = await response.json();

        status.innerHTML = "🟢 API Connected";

        const container = document.getElementById("products");

        container.innerHTML = "";

        products.forEach(product => {

            container.innerHTML += `

            <div class="card">

                <h2>${product.name}</h2>

                <p><strong>Category:</strong> ${product.categoryName}</p>

                <p class="price">$${product.price}</p>

            </div>

            `;

        });

    }

    catch(err){

        status.innerHTML="🔴 Cannot connect to API";

    }

}

loadProducts();