/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package entities;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Embeddable;
import javax.validation.constraints.NotNull;

/**
 *
 * @author Connor
 */
@Embeddable
public class OrderdetailsPK implements Serializable {
    @Basic(optional = false)
    @NotNull
    @Column(name = "products_productCode")
    private int productsproductCode;
    @Basic(optional = false)
    @NotNull
    @Column(name = "orders_orderNumber")
    private int ordersorderNumber;

    public OrderdetailsPK() {
    }

    public OrderdetailsPK(int productsproductCode, int ordersorderNumber) {
        this.productsproductCode = productsproductCode;
        this.ordersorderNumber = ordersorderNumber;
    }

    public int getProductsproductCode() {
        return productsproductCode;
    }

    public void setProductsproductCode(int productsproductCode) {
        this.productsproductCode = productsproductCode;
    }

    public int getOrdersorderNumber() {
        return ordersorderNumber;
    }

    public void setOrdersorderNumber(int ordersorderNumber) {
        this.ordersorderNumber = ordersorderNumber;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (int) productsproductCode;
        hash += (int) ordersorderNumber;
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof OrderdetailsPK)) {
            return false;
        }
        OrderdetailsPK other = (OrderdetailsPK) object;
        if (this.productsproductCode != other.productsproductCode) {
            return false;
        }
        if (this.ordersorderNumber != other.ordersorderNumber) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "entities.OrderdetailsPK[ productsproductCode=" + productsproductCode + ", ordersorderNumber=" + ordersorderNumber + " ]";
    }
    
}
