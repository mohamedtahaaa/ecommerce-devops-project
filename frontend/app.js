async function loadProducts() {

    const status = document.getElementById("status");

    try {

        const response = await fetch("/api/products");

        const result = await response.json();

        const products = result.data.items;

        status.innerHTML = "🟢 API Connected";

        const container = document.getElementById("products");

        container.innerHTML = "";

        products.forEach(product => {

            container.innerHTML += `

                <div class="card">

                    <h2>${product.name}</h2>

                    <p>${product.description}</p>

                    <p><strong>Category:</strong> ${product.category.name}</p>

                    <p><strong>Stock:</strong> ${product.stockQuantity}</p>

                    <p class="price">$${product.price}</p>

                </div>

            `;

        });

    }

    catch(err){

        console.error(err);

        status.innerHTML = "🔴 Cannot connect to API";

    }

}

loadProducts();