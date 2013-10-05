/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package entities;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Connor
 */
@Entity
@Table(name = "orderdetails")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Orderdetails.findAll", query = "SELECT o FROM Orderdetails o"),
    @NamedQuery(name = "Orderdetails.findByProductsproductCode", query = "SELECT o FROM Orderdetails o WHERE o.orderdetailsPK.productsproductCode = :productsproductCode"),
    @NamedQuery(name = "Orderdetails.findByOrdersorderNumber", query = "SELECT o FROM Orderdetails o WHERE o.orderdetailsPK.ordersorderNumber = :ordersorderNumber"),
    @NamedQuery(name = "Orderdetails.findByQuanityOrdered", query = "SELECT o FROM Orderdetails o WHERE o.quanityOrdered = :quanityOrdered"),
    @NamedQuery(name = "Orderdetails.findByPriceEach", query = "SELECT o FROM Orderdetails o WHERE o.priceEach = :priceEach")})
public class Orderdetails implements Serializable {
    private static final long serialVersionUID = 1L;
    @EmbeddedId
    protected OrderdetailsPK orderdetailsPK;
    @Basic(optional = false)
    @NotNull
    @Column(name = "quanityOrdered")
    private int quanityOrdered;
    @Basic(optional = false)
    @NotNull
    @Column(name = "priceEach")
    private double priceEach;
    @JoinColumn(name = "orders_orderNumber", referencedColumnName = "orderNumber", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Orders orders;
    @JoinColumn(name = "products_productCode", referencedColumnName = "productCode", insertable = false, updatable = false)
    @ManyToOne(optional = false)
    private Products products;

    public Orderdetails() {
    }

    public Orderdetails(OrderdetailsPK orderdetailsPK) {
        this.orderdetailsPK = orderdetailsPK;
    }

    public Orderdetails(OrderdetailsPK orderdetailsPK, int quanityOrdered, double priceEach) {
        this.orderdetailsPK = orderdetailsPK;
        this.quanityOrdered = quanityOrdered;
        this.priceEach = priceEach;
    }

    public Orderdetails(int productsproductCode, int ordersorderNumber) {
        this.orderdetailsPK = new OrderdetailsPK(productsproductCode, ordersorderNumber);
    }

    public OrderdetailsPK getOrderdetailsPK() {
        return orderdetailsPK;
    }

    public void setOrderdetailsPK(OrderdetailsPK orderdetailsPK) {
        this.orderdetailsPK = orderdetailsPK;
    }

    public int getQuanityOrdered() {
        return quanityOrdered;
    }

    public void setQuanityOrdered(int quanityOrdered) {
        this.quanityOrdered = quanityOrdered;
    }

    public double getPriceEach() {
        return priceEach;
    }

    public void setPriceEach(double priceEach) {
        this.priceEach = priceEach;
    }

    public Orders getOrders() {
        return orders;
    }

    public void setOrders(Orders orders) {
        this.orders = orders;
    }

    public Products getProducts() {
        return products;
    }

    public void setProducts(Products products) {
        this.products = products;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (orderdetailsPK != null ? orderdetailsPK.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Orderdetails)) {
            return false;
        }
        Orderdetails other = (Orderdetails) object;
        if ((this.orderdetailsPK == null && other.orderdetailsPK != null) || (this.orderdetailsPK != null && !this.orderdetailsPK.equals(other.orderdetailsPK))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "entities.Orderdetails[ orderdetailsPK=" + orderdetailsPK + " ]";
    }
    
}
